using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseState : MonsterBaseState
{
    private bool isReady = false;

    public Vector3 currentPosition;
    public Vector3 Attack2m;
    public Vector3 Attack4m;



    public MonsterChaseState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("상태는 변함");

        timer = 0.7f;

        StartAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
        currentPosition = stateMachine.attackRenderer.transform.localPosition;

        Attack2m = new Vector3(stateMachine.attackRenderer.transform.localPosition.x + 0.5f,
                               stateMachine.attackRenderer.transform.localPosition.y,
                               stateMachine.attackRenderer.transform.localPosition.z);

        Attack4m = new Vector3(stateMachine.attackRenderer.transform.localPosition.x + 1f,
                               stateMachine.attackRenderer.transform.localPosition.y,
                               stateMachine.attackRenderer.transform.localPosition.z);
    }

    public override void Exit()
    {
        isReady = false;
        StopAnimation(stateMachine.Monster.AnimationData.AttackReadyParameterHash);
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
        ReadyToAttack_2M();

        if (!isReady) return;
        timer -= Time.deltaTime;
    }

    public void StartChasing()
    {
        //기본적인 추적 로직, 공격 준비 중일 땐 추적을 멈추기 위해 bool값을 사용했습니다.
        if (!isReady && stateMachine.monsterGroundChecker.IsGroundChecker())
        {
            Vector3 dir = (stateMachine.targetTransform.position - stateMachine.ownerTransform.position).normalized;

            Debug.Log("들어가나?");
            stateMachine.ownerTransform.position += dir * stateMachine.Monster.MonsterData.MoveSpeed * Time.deltaTime;
        }

        //공격 거리 탐색 후 2M터 일 경우 bool값으로 트리거 해줍니다.
        if (Vector3.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < attackDIstance_2m && !isReady)
        {
            Debug.Log("공격준비");

            isReady = true;

            StopAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
            StartAnimation(stateMachine.Monster.AnimationData.AttackReadyParameterHash);
        }

        //8M이상 멀어지면 Return상태(제자리로 돌아가기)로 변환 시켜줍니다.
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < missingDistance) return;

        stateMachine.ChangeState(stateMachine.ReturnState);
    }

    // 공격전 모션 실행하는 메서드 입니다.
    public void ReadyToAttack_2M()
    {
        if (!isReady) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);    //히트박스표시 사이즈 조절 통해서 범위 추가 가능

        stateMachine.attackRenderer.transform.localScale = new Vector3(2f, 1f, 1f);
        stateMachine.attackRenderer.transform.localPosition = Attack2m;

        if (timer > 0) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);

        Debug.Log("공격모션까진 들어감");
        stateMachine.ChangeState(stateMachine.AttackState);
    }
}