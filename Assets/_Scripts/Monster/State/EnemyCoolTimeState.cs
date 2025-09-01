using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoolTimeState : EnemyBaseState
{
    public EnemyCoolTimeState(EnemyStateMachine stateMachine) : base(stateMachine)
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
    }

    public void StartCoolTime()
    {
        //TODO : 쿨타임 대기 로직 작성
    }
}
