using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }

    public override void Enter()
    {
        Debug.Log("아이들");
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
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
        TargetSearching();
    }

    //주위의 적이 있는지 서칭하는 메서드 입니다.
    public void TargetSearching()
    {
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) > trackingDistance) return;

        stateMachine.ChangeState(stateMachine.ChaseState);
    }
}
