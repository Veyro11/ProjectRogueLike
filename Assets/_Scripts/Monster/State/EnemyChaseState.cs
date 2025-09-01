using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    float speed = 0.8f;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
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
        StartChasing();
    }

    public void StartChasing()
    {
        //TODO : 플레이어 트렌스폼을 통해 방향 설정 후 정해진 속도로 추적 OR NAV MESH 2D 구현 방법 찾기
        Vector3 dir = (stateMachine.targetTransform.position - stateMachine.ownerTransform.position).normalized;

        stateMachine.ownerTransform.position += dir * speed * Time.deltaTime;

        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < missingDistance) return;

        stateMachine.ChangeState(stateMachine.ReturnState);
    }
}
