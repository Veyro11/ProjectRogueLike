using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);

        if (stateMachine.Player.Rb.velocity.y > 0)
        {
            StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
        }
        else
        {
            StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (stateMachine.Player.Rb.velocity.y <= 0 && stateMachine.Player.IsGrounded())
        {
            if (stateMachine.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
            else
            {
                stateMachine.ChangeState(
                    stateMachine.IsRunning
                    ? stateMachine.RunState
                    : stateMachine.WalkState
                );
            }
            return;
        }

        if (stateMachine.MovementInput.x != 0)
        {
            if (IsWallInFront(out RaycastHit2D hit))
            {
                if (CanClimbLedge(hit, out Vector2 ledgePosition))
                {
                    stateMachine.LedgePosition = ledgePosition;
                    stateMachine.ChangeState(stateMachine.ClimbingState);
                }
            }
        }
        
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.Rb.velocity.y <= 0)
        {
            StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
        }
    }
}