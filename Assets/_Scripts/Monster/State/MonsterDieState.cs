using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : MonsterBaseState
{
    public MonsterDieState(MonsterStateMachine stateMachine) : base(stateMachine)
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
        // 아직 아이템이 없어서 나온다면 드랍하도록 적용 해 주면 될 것 같습니다.
        Object.Destroy(stateMachine.Monster.gameObject);
        Debug.Log("보스사망 전" + Player.Instance.playerstat.MaxSouls);
        Player.Instance.playerstat.SetMaxSouls(3);

        Debug.Log("보스사망" + Player.Instance.playerstat.MaxSouls);
    }
}
