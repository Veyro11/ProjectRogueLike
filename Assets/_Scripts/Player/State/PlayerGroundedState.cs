using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
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

        AddInputActionsCallbacks();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);

        RemoveInputActionsCallbacks();
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        stateMachine.Player.Input.playerActions.Jump.started += OnJumpStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.playerActions.Jump.started -= OnJumpStarted;
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.Player.IsGrounded())
        {
            stateMachine.ChangeState(stateMachine.AirState);
        }
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
}