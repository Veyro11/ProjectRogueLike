using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyApplyForce;
    private AttackInfoData attackInfoData;

    private List<Collider2D> HitEnemi;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        stateMachine.WantsToContinueCombo = false;
        alreadyApplyForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttakData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);

        HitEnemi = new List<Collider2D>();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");

        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }

            if (normalizedTime >= attackInfoData.Dealing_Start_TransitionTime &&
                normalizedTime <= attackInfoData.Dealing_End_TransitionTime)
            {
                TryDealDamage();
            }
        }
        else
        {
            if (stateMachine.WantsToContinueCombo && attackInfoData.ComboStateIndex != -1)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ComboIndex = 0;
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void TryDealDamage()
    {
        Transform attackRange = stateMachine.Player.AttackRange;
        Vector2 attackSize = stateMachine.Player.AttackSize;
        LayerMask monsterLayer = stateMachine.Player.MonsterLayer;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackRange.position, attackSize, 0f, monsterLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            if (!HitEnemi.Contains(enemyCollider))
            {
                //if (몬스터체크)
                //{
                //    monster.TakeDamage(attackInfoData.Damage);
                //}

                HitEnemi.Add(enemyCollider);
            }
        }
    }

    protected override void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.WantsToContinueCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        Vector2 forceDirection = stateMachine.Player.transform.right * stateMachine.Player.transform.localScale.x;
        stateMachine.Player.ForceReceiver.AddForce(forceDirection * attackInfoData.Force);
    }
}