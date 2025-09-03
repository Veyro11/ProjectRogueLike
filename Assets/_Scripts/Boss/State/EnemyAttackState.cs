using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackState : EnemyBaseState
{
    private Action currentAttackPattern;
    private bool animationStarted = false;
    private float triggerStart;
    private bool isTrigger;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("공격시작");
        animationStarted = false;
        isTrigger = false;

        timer = 0.75f;

        triggerStart = 0.5f;

        ChooseAttack();

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
        DieCheck();
        currentAttackPattern?.Invoke();
        triggerStart -= Time.deltaTime;
    }

    public override void OntriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer != LayerMask.NameToLayer("Player")) return;

        //Debug.Log(Player.Instance.currentHealth);
        stateMachine.attackRenderer.transform.localPosition = stateMachine.ChaseState.currentPosition;
        stateMachine.Enemy.attackCollider2D.enabled = false;
        Player.Instance.TakeDamage(stateMachine.Enemy.EnemyData.Attack);
    }

    private void AttackPattern_2M()
    {
        timer -= Time.deltaTime;

        if (triggerStart <= 0 && !isTrigger)
        {
            stateMachine.Enemy.attackCollider2D.enabled = true;
            isTrigger = true;
        }

        if (!animationStarted)
        {
            //공격 범위 콜라이더 사이즈 초기화 해주는 부분 입니다.
            //stateMachine.attackCollider.size = new Vector2(1.5f, stateMachine.attackCollider.size.y);
            
            //Debug.Log(stateMachine.attackCollider.size);
            StartAnimation(stateMachine.Enemy.AnimationData.Attack_1_ParameterHash);
        }

        if (timer <= 0f)
        {
            stateMachine.attackRenderer.transform.localPosition = stateMachine.ChaseState.currentPosition;
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    private void AttackPattern_4M()
    {
        timer -= Time.deltaTime;


        if (triggerStart <= 0 && !isTrigger)
        {
            stateMachine.Enemy.attackCollider2D.enabled = true;
            isTrigger = true;
        }

        if (!animationStarted)
        {
            //공격 범위 콜라이더 사이즈 초기화 해주는 부분 입니다.
            //stateMachine.attackCollider.size = new Vector2(2.5f, stateMachine.attackCollider.size.y);
           
            //Debug.Log(stateMachine.attackCollider.size);
            StartAnimation(stateMachine.Enemy.AnimationData.Attack_2_ParameterHash);
        }

        if (timer <= 0f)
        {
            stateMachine.attackRenderer.transform.localPosition = stateMachine.ChaseState.currentPosition;
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

    //랜덤공격 시 50퍼센트 확률로 공격모션을 골라주는 메서드 입니다.
    private void ChooseAttack()
    {
        if (stateMachine.ChaseState.is_2M_AttackReady)
        {
            currentAttackPattern = AttackPattern_2M ;
        }
        else if (stateMachine.ChaseState.is_4M_AttackReady)
        {
            currentAttackPattern = AttackPattern_4M;
        }
    }
    
}
