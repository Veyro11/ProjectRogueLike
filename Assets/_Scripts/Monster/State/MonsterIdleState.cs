using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    private bool isWalking = false;
    private float patrolRange = 2f; // 좌우 이동 범위
    private Vector3 startPos;
    private int direction = 3; // 1 = 오른쪽, -1 = 왼쪽
    

    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }

    public override void Enter()
    {

        isWalking = false;
        timer = Random.Range(2f, 5f);

        Debug.Log("아이들");
        startPos = stateMachine.ownerTransform.position;
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
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
        Patrol();
    }

    //주위의 적이 있는지 서칭하는 메서드 입니다.
    public void TargetSearching()
    {
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) > trackingDistance) return;

        stateMachine.ChangeState(stateMachine.ChaseState);
    }

    public void Patrol()
    {
        timer -= Time.deltaTime;

        if (!isWalking)
        {
            // Idle 상태 유지
            if (timer <= 0)
            {
                StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
                StartAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);

                isWalking = true;
                timer = 3f; // 순찰 도는 시간
            }
        }
        else
        {
            // Walk 상태
            stateMachine.ownerTransform.Translate(Vector2.right * direction * stateMachine.Monster.MonsterData.MoveSpeed * Time.deltaTime);

            if (Mathf.Abs(stateMachine.ownerTransform.position.x - startPos.x) >= patrolRange)
            {
                direction *= -1; // 방향 반전
            }

            if (timer <= 0)
            {
                stateMachine.ChangeState(stateMachine.ReturnState);
            }
        }
    }

    public override void LookPlayer()
    {
        if (isWalking)
        {
            // 순찰중이면 이동 방향 바라보기
            if (direction > 0)
                stateMachine.Monster.transform.localScale = new Vector3(1, 1, 1);
            else
                stateMachine.Monster.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // 대기중이면 플레이어 바라보기
            Vector3 lookDirection = stateMachine.targetTransform.position - stateMachine.Monster.transform.position;

            if (lookDirection.x > 0)
                stateMachine.Monster.transform.localScale = new Vector3(1, 1, 1);
            else
                stateMachine.Monster.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
