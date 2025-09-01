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

    }

    public override void Exit()
    {

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
        // TODO : IDLE 애니메이션 필요
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) > trackingDistance) return;

        stateMachine.ChangeState(stateMachine.ChaseState);
    }
}
