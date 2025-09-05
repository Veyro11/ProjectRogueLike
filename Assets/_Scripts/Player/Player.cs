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
    [field: SerializeField] public SpriteRenderer PlayerSpriteRenderer { get; private set; }

    public ForceReceiver ForceReceiver { get; private set; }

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem particle;

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

    public float currentHealth;

    private int playerLayer;
    private int playerDashLayer;

    public PlayerStatus playerstat;

    public bool pause { get; private set; }
    public bool playerDie { get; private set; }

    private void Awake()
    {
        //초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        AnimationData.Initialize();

        stateMachine = new PlayerStateMachine(this);
        
        Input = GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();
        ForceReceiver = GetComponent<ForceReceiver>();

        Animator = GetComponentInChildren<Animator>();
        PlayerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SpriteTransform = transform.Find("Sprite");

        playerLayer = LayerMask.NameToLayer("Player");
        playerDashLayer = LayerMask.NameToLayer("PlayerDash");

        playerstat = GetComponent<PlayerStatus>();
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

    public void PauseUser(bool a)
    {
        pause = a;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerstat.MaxHealth);

        Debug.Log($"피해량체크- {damage} 남은체력- {currentHealth}");
        BarEventManager.Instance.HPBarCall(currentHealth+damage, currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HitColor());
        }

    }

    private IEnumerator HitColor()
    {
        float a = 1f;
        float b = 0.1f;

        Color color = PlayerSpriteRenderer.color;

        SetLayer(true);
        float time = 0f;
        while (time < a)
        {
            PlayerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(b);
            time += b;

            PlayerSpriteRenderer.color = color;
            yield return new WaitForSeconds(b);
            time += b;
        }

        SetLayer(false);
        PlayerSpriteRenderer.color = color;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, Data.MaxHealth);

        Debug.Log($"체력회복");
    }

    private void Die()
    {
        if (playerDie) return;

        Animator.SetBool("Die", true);
        playerDie = true;
        Debug.Log("YOU DIE");
        stateMachine.ChangeState(stateMachine.DieState);
        UIManager.Instance.SetGameOver();
    }

    public void Revive()
    {
        PauseUser(true);
        Animator.SetBool("Die", false);
        currentHealth = playerstat.MaxHealth;
        BarEventManager.Instance.HPBarCall(0, currentHealth);
        playerDie = false;
        stateMachine.ChangeState(stateMachine.IdleState);
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

    public void SetLayer(bool isDashing)
    {
        if (isDashing)
        {
            gameObject.layer = playerDashLayer;
        }
        else
        {
            gameObject.layer = playerLayer;
        }
    }

}