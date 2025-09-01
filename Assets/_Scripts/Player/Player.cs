using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Transform SpriteTransform { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    private PlayerStateMachine stateMachine;

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
    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();

        stateMachine = new PlayerStateMachine(this);

        SpriteTransform = transform.Find("Sprite");

        ForceReceiver = GetComponent<ForceReceiver>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
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
}