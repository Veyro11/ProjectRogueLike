using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackState : EnemyBaseState
{
    private Action currentAttackPattern;
    //private bool animationStarted = false;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("?");
        timer = stateMachine.Enemy.EnemyData.AttackCoolTime;
        ChooseRandomAttack();
        //animationStarted = false;
        stateMachine.Enemy.attackCollider2D.enabled = true;
    }

    public override void Exit()
    {
        currentAttackPattern = null;
        stateMachine.Enemy.attackCollider2D.enabled = false;
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
        currentAttackPattern?.Invoke();
    }

    public override void OntriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("트리거");
        stateMachine.Enemy.attackCollider2D.enabled = false;
        //이제 트리거 안에 랜덤으로 공격 하는 메서드 하나 넣어주면 끝

    }

    private void AttackPattern_2M()
    {
        //if (!animationStarted)
        //{
        //    //애니메이션 한번 실행
        //}

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    private void AttackPattern_4M()
    {
        //if (!animationStarted)
        //{
        //    //애니메이션 한번 실행
        //}

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    private void ChooseRandomAttack()
    {
        int random = Random.Range(0, 2);
        Debug.Log(random);
        switch (random)
        {
            case 0:
                currentAttackPattern = AttackPattern_2M;
                Debug.Log("2M");
                break;
            case 1:
                currentAttackPattern = AttackPattern_4M;
                Debug.Log("4M");
                break;
        }
    }
}
