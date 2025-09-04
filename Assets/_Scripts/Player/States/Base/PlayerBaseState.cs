using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    protected virtual void AddInputActionsCallbacks()
    {
        if (!Player.Instance.pause)
            return;

        PlayerController input = stateMachine.Player.Input;
        input.playerActions.Movement.canceled += OnMovementCanceled;

        input.playerActions.Dash.started += OnDashStarted;
        input.playerActions.Dash.canceled += OnDashCanceled;

        stateMachine.Player.Input.playerActions.Attack.performed += OnAttackPerformed;
        stateMachine.Player.Input.playerActions.Attack.canceled += OnAttackCanceled;

        stateMachine.Player.Input.playerActions.Jump.started += OnJumpStarted;
    }
    

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.playerActions.Movement.canceled -= OnMovementCanceled;

        input.playerActions.Dash.started -= OnDashStarted;
        input.playerActions.Dash.canceled -= OnDashCanceled;

        stateMachine.Player.Input.playerActions.Attack.performed -= OnAttackPerformed;
        stateMachine.Player.Input.playerActions.Attack.canceled -= OnAttackCanceled;

        stateMachine.Player.Input.playerActions.Jump.started -= OnJumpStarted;
    }
    

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.DashState);
    }

    protected virtual void OnDashCanceled(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void Update()
    {

    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.playerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        float inputX = stateMachine.MovementInput.x;
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Rb.velocity = new Vector2(inputX * movementSpeed, stateMachine.Player.Rb.velocity.y);
        //Debug.Log(movementSpeed + " , "+inputX);

        Rotate(inputX);
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(float directionX)
    {
        if (directionX != 0)
        {
            bool isFacingRight = directionX > 0;

            if (isFacingRight != (stateMachine.Player.transform.localScale.x > 0))
            {
                stateMachine.Player.transform.localScale = new Vector3(Mathf.Sign(directionX), 1, 1);
            }
        }
    }



    protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
        Debug.Log("Attack!");
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }


    protected bool IsWallInFront(out RaycastHit2D hit)
    {
        Vector2 direction = new Vector2(stateMachine.Player.transform.localScale.x, 0);
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        float distance = stateMachine.Player.WallCheckDistance;

        Vector2 lowerOrigin = stateMachine.Player.WallCheck.position;
        RaycastHit2D lowerHit = Physics2D.Raycast(lowerOrigin, direction, distance, groundLayer);
        Debug.DrawRay(lowerOrigin, direction * distance, Color.red);

        Vector2 upperOrigin = stateMachine.Player.WallCheckUpper.position;
        RaycastHit2D upperHit = Physics2D.Raycast(upperOrigin, direction, distance, groundLayer);
        Debug.DrawRay(upperOrigin, direction * distance, Color.yellow);

        if (lowerHit.collider != null)
        {
            hit = lowerHit;
            return true;
        }
        if (upperHit.collider != null)
        {
            hit = upperHit;
            return true;
        }

        hit = new RaycastHit2D();
        return false;
    }

    protected bool CanClimbLedge(RaycastHit2D wallHit, out Vector2 ledgePosition)
    {
        ledgePosition = Vector2.zero;
        Transform playerTransform = stateMachine.Player.transform;

        float ledgeSearchDistance = 2f;

        Vector2 ledgeCheckStart = new Vector2(wallHit.point.x + 0.1f * playerTransform.localScale.x, playerTransform.position.y + 1f);
        Debug.DrawRay(ledgeCheckStart, Vector2.down * ledgeSearchDistance, Color.blue);
        RaycastHit2D ledgeHit = Physics2D.Raycast(ledgeCheckStart, Vector2.down, ledgeSearchDistance, LayerMask.GetMask("Ground"));

        if (ledgeHit.collider != null)
        {
            float ledgeHeight = ledgeHit.point.y;
            float playerRefHeight = stateMachine.Player.WallCheck.position.y;
            float heightDifference = ledgeHeight - playerRefHeight;

            if (heightDifference > 0 && heightDifference <= stateMachine.Player.WallClimbHeight)
            {
                ledgePosition = ledgeHit.point + new Vector2(0, 1f);
                return true;
            }
        }

        return false;
    }


    public void OntriggerEnter2D(Collider2D collision)  //TODO : 나중에 수정해야함
    {
        
    }
}