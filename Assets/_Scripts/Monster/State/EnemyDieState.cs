using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState
{
    public EnemyDieState(EnemyStateMachine stateMachine) : base(stateMachine)
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

    public void Dying()
    {
        //TODO : 죽었을 때 로직 작성
        Object.Destroy(stateMachine.Enemy.gameObject);
        Debug.Log("보스사망");
    }
}
