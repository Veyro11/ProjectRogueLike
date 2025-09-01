using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private bool isReady = false;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        isReady = false;
        timer = 0.7f;
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
        StartAttack();

        if (!isReady) return;
        timer -= Time.deltaTime;
    }

    public void StartChasing()
    {
        //TODO : 플레이어 트렌스폼을 통해 방향 설정 후 정해진 속도로 추적 OR NAV MESH 2D 구현 방법 찾기

        if (!isReady)
        {
            Vector3 dir = (stateMachine.targetTransform.position - stateMachine.ownerTransform.position).normalized;

            stateMachine.ownerTransform.position += dir * stateMachine.Enemy.EnemyData.MoveSpeed * Time.deltaTime;
        }



        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < missingDistance) return;

        stateMachine.ChangeState(stateMachine.ReturnState);
    }

    public void StartAttack()
    {
        //TODO : 필요할 경우 조건 추가
        if (Vector3.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) > attackDIstance_2m) return;

        isReady = true;
        Attacking();
    }

    public void Attacking()
    {
        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);    //히트박스표시 사이즈 조절 통해서 범위 추가 가능

        if (timer > 0) return;

        Debug.Log("공격시작");
        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
        stateMachine.ChangeState(stateMachine.AttackState);
    }
}
