using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : EnemyBaseState
{
    public EnemyReturnState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        MissingTarget();
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

    public void MissingTarget()
    {
        Debug.Log("return");
        //TODO : 거리 계산 후 Return으로 전환 로직 작성
    }
}
