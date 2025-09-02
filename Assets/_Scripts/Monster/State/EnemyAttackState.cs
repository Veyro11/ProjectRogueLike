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
        animationStarted = false;
        stateMachine.Enemy.attackCollider2D.enabled = true;
        timer = stateMachine.Enemy.EnemyData.AttackCoolTime;

        // 2M에서 공격 했을 경우 랜덤 공격 4M에서 봤을 경우 4M공격 고정하는 조건문 입니다.
        if (stateMachine.ChaseState.is_2M_Attack)
        {
            ChooseRandomAttack();
        }
        else if (stateMachine.ChaseState.is_4M_Attack)
        {
            currentAttackPattern = AttackPattern_4M;
        }

        Debug.Log(stateMachine.ChaseState.is_2M_Attack.ToString() + stateMachine.ChaseState.is_4M_Attack.ToString());
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

        //Debug.Log(Player.Instance.currentHealth);
        stateMachine.Enemy.attackCollider2D.enabled = false;
        Player.Instance.TakeDamage(stateMachine.Enemy.EnemyData.Attack);
    }

    private void AttackPattern_2M()
    {
        timer -= Time.deltaTime;

        if (!animationStarted)
        {
            //공격 범위 콜라이더 사이즈 초기화 해주는 부분 입니다.
            stateMachine.attackCollider.size = new Vector2(2, stateMachine.attackCollider.size.y);
            
            //Debug.Log(stateMachine.attackCollider.size);
            StartAnimation(stateMachine.Enemy.AnimationData.Attack_1_ParameterHash);
        }

        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    private void AttackPattern_4M()
    {
        timer -= Time.deltaTime;

        if (!animationStarted)
        {
            //공격 범위 콜라이더 사이즈 초기화 해주는 부분 입니다.
            stateMachine.attackCollider.size = new Vector2(4, stateMachine.attackCollider.size.y);
           
            //Debug.Log(stateMachine.attackCollider.size);
            StartAnimation(stateMachine.Enemy.AnimationData.Attack_2_ParameterHash);
        }

        if (timer <= 0f)
        {
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    //랜덤공격 시 50퍼센트 확률로 공격모션을 골라주는 메서드 입니다.
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
