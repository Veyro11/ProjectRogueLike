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
        stateMachine.attackRenderer.GetComponent<BoxCollider2D>();
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

    public override void Ontrigger2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        //플레이어 HP까는 로직 아직 플레이어 HP가 없음 Test용
        collision.gameObject.SetActive(false); 
    }

    public  void Attacking()
    {
        //TODO : 공격로직 작성 및 랜덤으로 공격 로직 작성
        timer -= Time.deltaTime;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);    //히트박스표시 사이즈 조절 통해서 범위 추가 가능

        if (timer > 0) return;

        Debug.Log("공격시작");
        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
        stateMachine.ChangeState(stateMachine.CoolTimeState);

    }
}
