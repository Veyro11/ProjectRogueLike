using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyReturnState : EnemyBaseState
{
    Vector3 lookDirection;

    public EnemyReturnState(EnemyStateMachine stateMachine) : base(stateMachine)
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
        MissingTarget();
    }

    //public void MissingTarget()
    //{
    //    if (stateMachine.ownerTransform.position == stateMachine.spawnPosition) return;

    //    Vector3 returnPosition = (stateMachine.spawnPosition - stateMachine.ownerTransform.position).normalized;
    //    stateMachine.ownerTransform.position += returnPosition * 2 * Time.deltaTime;
    //    Debug.Log("return");
    //    //TODO : 거리 계산 후 Return으로 전환 로직 작성
    //}

    public void MissingTarget()
    {
        // 현재 위치에서 spawnPosition으로 이동
        stateMachine.ownerTransform.position = Vector3.MoveTowards
        (
            stateMachine.ownerTransform.position,
            stateMachine.spawnPosition,
            2 * Time.deltaTime
        );

        // 목표 지점에 도착했는지 체크
        if (stateMachine.ownerTransform.position == stateMachine.spawnPosition)
        {
            Debug.Log("원래 자리 도착!");

            // TODO: 상태 전환 로직 실행
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void LookPlayer()
    {
        lookDirection = stateMachine.spawnPosition - stateMachine.Enemy.transform.position;

        if (lookDirection.x > 0)
        {
            // 오른쪽을 바라보게
            stateMachine.Enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 왼쪽을 바라보게
            stateMachine.Enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
