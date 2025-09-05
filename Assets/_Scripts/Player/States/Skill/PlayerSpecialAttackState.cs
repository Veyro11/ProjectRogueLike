using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttackState : PlayerBaseState
{
    private float animationDuration = 1.5f;
    private float startTime;

    public PlayerSpecialAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
        stateMachine.MovementSpeedModifier = 0f;

        stateMachine.Player.playerstat.AddSP(-stateMachine.Player.playerstat.MaxSP);
        BarEventManager.Instance.SPBarCall(stateMachine.Player.playerstat.MaxSP, 0);

        StartAnimation(stateMachine.Player.AnimationData.SpecialAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.SpecialAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "SpecialAttack");
        if (normalizedTime > 0.5f && normalizedTime < 0.8f)
        {
            DealDamage();
        }

        if (Time.time >= startTime + animationDuration)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    private void DealDamage()
    {
        Collider2D[] allMonsters = GameObject.FindObjectsOfType<Collider2D>();
        foreach (Collider2D monsterCol in allMonsters)
        {
            if (monsterCol.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                float specialDamage = stateMachine.Player.playerstat.AttackPower * 5f;

                Monster monster = monsterCol.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.TakeDamage(specialDamage);
                }

                Boss boss = monsterCol.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage(specialDamage);
                }
            }
        }

    }
}