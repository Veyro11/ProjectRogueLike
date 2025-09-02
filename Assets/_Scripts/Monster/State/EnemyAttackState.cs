using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackState : EnemyBaseState
{
    private Action currentAttackPattern;
    private bool animationStarted = false;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("공격시작");
        timer = stateMachine.Enemy.EnemyData.AttackCoolTime;
        ChooseRandomAttack();
        animationStarted = false;
        stateMachine.Enemy.attackCollider2D.enabled = true;
    }

    public override void Exit()
    {
        currentAttackPattern = null;
        stateMachine.Enemy.attackCollider2D.enabled = false;
        StopAnimation(stateMachine.Enemy.AnimationData.Attack_1_ParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.Attack_2_ParameterHash);
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
        //collision.gameObject.SetActive(false);
        //이제 트리거 안에 랜덤으로 공격 하는 메서드 하나 넣어주면 끝

    }

    private void AttackPattern_2M()
    {
        if (!animationStarted)
        {
            StartAnimation(stateMachine.Enemy.AnimationData.Attack_1_ParameterHash);
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    private void AttackPattern_4M()
    {
        if (!animationStarted)
        {
            StartAnimation(stateMachine.Enemy.AnimationData.Attack_2_ParameterHash);
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    private void ChooseRandomAttack()
    {
        int random = Random.Range(0, 2);
 
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
