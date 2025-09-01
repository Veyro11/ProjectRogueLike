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

    public void StartCoolTime() //TODO : 기획 의도 물어보기 구분해야 할지 안해야 할지 물어보고 합치라면 합치기 한곳에서 가능
    {
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if (timer <= 0f)
        {
            Debug.Log("다시 시작");
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
    }
}
