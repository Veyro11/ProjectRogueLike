using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    private PlayerStateMachine stateMachine;

    public ForceReceiver ForceReceiver { get; private set; }

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();

        stateMachine = new PlayerStateMachine(this);

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
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void DrawGroundCheckRay()
    {
        Color rayColor = IsGrounded() ? Color.green : Color.red;
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, rayColor);
    }
}