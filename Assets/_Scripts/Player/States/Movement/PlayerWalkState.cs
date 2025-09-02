using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

        Player.Instance.SetEmission(true);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

        Player.Instance.SetEmission(false);
    }

    //public override void Update()
    //{
    //    base.Update();

    //    if (stateMachine.MovementInput == Vector2.zero)
    //    {
    //        stateMachine.ChangeState(stateMachine.IdleState);
    //    }
    //}

    //protected override void OnDashStarted(InputAction.CallbackContext context)
    //{
    //    base.OnDashStarted(context);
    //    stateMachine.ChangeState(stateMachine.RunState);
    //}
}