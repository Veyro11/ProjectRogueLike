using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDashState : PlayerBaseState
{
    private float dashStartTime;
    private PlayerDashData dashData;

    public PlayerDashState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        dashData = stateMachine.Player.Data.DashData;
    }

    public override void Enter()
    {
        base.Enter();

        dashStartTime = Time.time;
        stateMachine.Player.Animator.SetBool(stateMachine.Player.AnimationData.DashParameterHash, true);

        Vector2 forceDirection = stateMachine.Player.transform.right * stateMachine.Player.transform.localScale.x;
        stateMachine.Player.ForceReceiver.AddForce(forceDirection * dashData.DashForce);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.Animator.SetBool(stateMachine.Player.AnimationData.DashParameterHash, false);
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= dashStartTime + dashData.DashDuration)
        {
            if (stateMachine.Player.IsGrounded())
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
    }
}