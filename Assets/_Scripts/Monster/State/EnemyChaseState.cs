using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private bool isReady = false;
    public bool is_2M_Attack = false;
    public bool is_4M_Attack = false;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        timer = 0.7f;
        is_2M_Attack = false;
        is_4M_Attack = false;
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        isReady = false;
        StopAnimation(stateMachine.Enemy.AnimationData.AttackReadyParameterHash);
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
        ReadyToAttack();

        if (!isReady) return;
        timer -= Time.deltaTime;
    }

    public void StartChasing()
    {
        //기본적인 추적 로직, 공격 준비 중일 땐 추적을 멈추기 위해 bool값을 사용했습니다.
        if (!isReady && EnemyGroundChecker.Instance.IsGroundChecker())
        {
            Vector3 dir = (stateMachine.targetTransform.position - stateMachine.ownerTransform.position).normalized;

            stateMachine.ownerTransform.position += dir * stateMachine.Enemy.EnemyData.MoveSpeed * Time.deltaTime;
        }

        //공격 거리 탐색 후 2M터 일 경우 bool값으로 트리거 해줍니다.
        if (Vector3.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < attackDIstance_2m && !isReady)
        {
            Debug.Log("공격준비");
           
            isReady = true;
            is_2M_Attack = true;
           
            StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.AttackReadyParameterHash);
        }
        //공격 거리 탐색 후 4M터 일 경우 bool값으로 트리거 해줍니다.
        else if (Vector3.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < attackDIstance_4m && !isReady)
        {
            Debug.Log("공격준비");
           
            isReady = true;
            is_4M_Attack = true;
           
            StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.AttackReadyParameterHash);
        }

        //8M이상 멀어지면 Return상태(제자리로 돌아가기)로 변환 시켜줍니다.
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < missingDistance) return;

        stateMachine.ChangeState(stateMachine.ReturnState);
    }

    // 공격전 모션 실행하는 메서드 입니다.
    public void ReadyToAttack()
    {
        if (!isReady) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);    //히트박스표시 사이즈 조절 통해서 범위 추가 가능

        if (timer > 0) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
        stateMachine.ChangeState(stateMachine.AttackState);
    }
}
