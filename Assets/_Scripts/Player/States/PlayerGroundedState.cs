using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{

    private float groundTime = 0.1f;
    private float timeSet;

    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);

        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);

        base.AddInputActionsCallbacks();

        timeSet = groundTime;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);

        base.RemoveInputActionsCallbacks();
    }



    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.IsGrounded())
        {
            timeSet = groundTime;
        }
        else
        {
            timeSet -= Time.deltaTime;
        }

        if (timeSet <= 0f)
        {
            stateMachine.ChangeState(stateMachine.AirState);
        }
    }


    protected virtual void OnAttack()
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.MovementInput = context.ReadValue<Vector2>();

        if (stateMachine.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        base.OnJumpStarted(context);
        if (stateMachine.Player.IsGrounded())
        {
            stateMachine.ChangeState(stateMachine.JumpState);
        }
    }

    protected override void OnHealStarted(InputAction.CallbackContext context)
    {
        base.OnHealStarted(context);
        stateMachine.Player.playerstat.UsePotion();
    }

    protected override void OnSpecialAttackStarted(InputAction.CallbackContext context)
    {
        base.OnSpecialAttackStarted(context);

        stateMachine.ChangeState(stateMachine.SpecialAttackState);

        if (stateMachine.Player.playerstat.CurSP >= stateMachine.Player.playerstat.MaxSP)
        {
            stateMachine.ChangeState(stateMachine.SpecialAttackState);
        }
        else
        {
            Debug.Log("SP부족");
        }
    }
}