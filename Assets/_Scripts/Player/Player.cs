using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Transform SpriteTransform { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    private PlayerStateMachine stateMachine;

    [SerializeField] private ParticleSystem particle;

    public ForceReceiver ForceReceiver { get; private set; }

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    [field: Header("Wall Check")]
    [field: SerializeField] public Transform WallCheck { get; private set; }
    [field: SerializeField] public Transform WallCheckUpper { get; private set; }
    [field: SerializeField] public float WallCheckDistance { get; private set; } = 0.6f;
    [field: SerializeField] public float WallClimbHeight { get; private set; } = 1.5f;

    [field: Header("Attack")]
    [field: SerializeField] public Transform AttackRange { get; private set; }
    [field: SerializeField] public Vector2 AttackSize { get; private set; } = new Vector2(1.5f, 1f);
    [field: SerializeField] public LayerMask MonsterLayer { get; private set; }

    public GameObject slashPrefab;

    public GameObject dashPrefab;

    private float currentHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //초기화
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();

        stateMachine = new PlayerStateMachine(this);

        SpriteTransform = transform.Find("Sprite");

        ForceReceiver = GetComponent<ForceReceiver>();

        var emission = particle.emission;
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);

        currentHealth = Data.MaxHealth;
        //마우스 커서 숨기기 일단 꺼둠
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();

        DrawGroundCheckRay();
    }


    public float GetAttackDamage()
    {
        return Data.AttackPower;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, Data.MaxHealth);

        Debug.Log($"피해량체크- {damage} 남은체력- {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, Data.MaxHealth);

        Debug.Log($"체력회복");
    }

    private void Die()
    {
        Debug.Log("YOU DIE");
        // 게임오버 함수 추가
    }

    public bool IsGrounded()
    {
        bool isGroundedLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, groundCheckDistance, groundLayer);
        bool isGroundedRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, groundCheckDistance, groundLayer);

        return isGroundedLeft || isGroundedRight;
    }

    private void DrawGroundCheckRay()
    {
        bool isGroundedLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, groundCheckDistance, groundLayer);
        bool isGroundedRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, groundCheckDistance, groundLayer);

        Color leftRayColor = isGroundedLeft ? Color.green : Color.red;
        Color rightRayColor = isGroundedRight ? Color.green : Color.red;

        Debug.DrawRay(groundCheckLeft.position, Vector2.down * groundCheckDistance, leftRayColor);
        Debug.DrawRay(groundCheckRight.position, Vector2.down * groundCheckDistance, rightRayColor);
    }

    public void SetEmission(bool a)
    {
        var emission = particle.emission;
        emission.enabled = a;
    }

}