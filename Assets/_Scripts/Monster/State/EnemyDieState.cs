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
        Dying();
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
        
    }

    public void Dying()
    {
        //TODO : 죽었을 때 애니메이션 / 보상드랍 구현 필요
        Object.Destroy(stateMachine.Enemy.gameObject);
        Debug.Log("보스사망");
    }
}
