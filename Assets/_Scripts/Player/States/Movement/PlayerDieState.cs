using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.Player.Animator.CrossFade(stateMachine.Player.AnimationData.DieParameterHash, 0.1f);

        stateMachine.Player.Rb.velocity = Vector2.zero;

        RemoveInputActionsCallbacks();
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {
    }
}
