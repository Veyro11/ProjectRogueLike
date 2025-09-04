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
        // 아직 아이템이 없어서 나온다면 드랍하도록 적용 해 주면 될 것 같습니다.
        AudioManager.Instance.PlaySFX("Boss_Death");
        Object.Destroy(stateMachine.Enemy.gameObject);

        //Find로 찾을 때 SetActive가 false면 못찾아와서 찾아오게 바꿨습니다 나중에 캐싱 해 둘까요?
        GameObject secStagePortal = null;
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "SecStagePotal")
            {
                secStagePortal = obj;
                break;
            }
        }
        Debug.Log(secStagePortal);
        if (secStagePortal != null)
        {
            secStagePortal.SetActive(true);
        }

        Debug.Log("보스사망 전" + Player.Instance.playerstat.MaxSouls);
        Player.Instance.playerstat.SetMaxSouls(3);

        Debug.Log("보스사망" + Player.Instance.playerstat.MaxSouls);
    }
}
