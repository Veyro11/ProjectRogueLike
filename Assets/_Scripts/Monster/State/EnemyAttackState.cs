using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        timer = attackTime;
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
        Attacking();
    }

    public  void Attacking()
    {
        //TODO : 공격로직 작성
        timer -= Time.deltaTime;

        Debug.Log("공격시작");
        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);

        if (timer > 0) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
        stateMachine.ChangeState(stateMachine.CoolTimeState);
    }
}
