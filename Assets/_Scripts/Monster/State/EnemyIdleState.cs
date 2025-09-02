using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    public override void Enter()
    {
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void HandleInput()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void Update()
    {
        base.Update();
        TargetSearching();
    }

    public void TargetSearching()
    {
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) > trackingDistance) return;

            stateMachine.ChangeState(stateMachine.ChaseState);
    }
}
