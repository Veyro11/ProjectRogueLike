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
        Debug.Log("대기중");
        timer = coolTime;
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
        //base.Update();
        StartCoolTime();
    }

    //public IEnumerator StartCoolTime()    //코루틴으로 작성 했으나 사용 불가
    //{
    //    yield return new WaitForSeconds(coolTime);
    //    stateMachine.ChangeState(stateMachine.IdleState);
    //    Debug.Log("다시 시작");
    //}

    public void StartCoolTime()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
            Debug.Log("다시 시작");
        }
    }
}
