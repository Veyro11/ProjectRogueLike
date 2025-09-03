using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    private float triggerStart;
    private bool isTrigger;

    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("공격시작");
        isTrigger = false;

        timer = 0.75f;

        triggerStart = 0.5f;

    }

    public override void Exit()
    {
        stateMachine.Monster.attackCollider2D.enabled = false;
        StopAnimation(stateMachine.Monster.AnimationData.Attack_1_ParameterHash);
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
        AttackPattern_2M();
    }

    public override void OntriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer != LayerMask.NameToLayer("Player")) return;

        //Debug.Log(Player.Instance.currentHealth);
        stateMachine.attackRenderer.transform.localPosition = stateMachine.ChaseState.currentPosition;
        stateMachine.Monster.attackCollider2D.enabled = false;
        Player.Instance.TakeDamage(stateMachine.Monster.MonsterData.Attack);
    }

    private void AttackPattern_2M()
    {
        triggerStart -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (triggerStart <= 0 && !isTrigger)
        {
            stateMachine.Monster.attackCollider2D.enabled = true;
            isTrigger = true;
        }

        StartAnimation(stateMachine.Monster.AnimationData.Attack_1_ParameterHash);

        if (timer <= 0f)
        {
            stateMachine.attackRenderer.transform.localPosition = stateMachine.ChaseState.currentPosition;
            stateMachine.ChangeState(stateMachine.CoolTimeState);
        }
    }

}