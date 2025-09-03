using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoolTimeState : EnemyBaseState
{
    private int coolTime = 2;

    public EnemyCoolTimeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash); //약화 애니메이션 추가 필요
        Debug.Log("대기중");
        timer = coolTime;
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash); //약화 애니메이션 추가 필요
    }

    public override void HandleInput()
    {
        
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Update()
    {
        //base.Update();
        StartCoolTime();
    }

    //public IEnumerator StartCoolTime()    //코루틴으로 작성 했으나 사용 불가
    //{
    //    yield return new WaitForSeconds(coolTime);
    //    stateMachine.ChangeState(stateMachine.IdleState);
    //    Debug.Log("다시 시작");
    //}

    //쿨타임동안 정지 후 다시 추적모드로 전환하는 메서드 입니다.
    public void StartCoolTime()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Debug.Log("다시 시작");
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
    }
}
