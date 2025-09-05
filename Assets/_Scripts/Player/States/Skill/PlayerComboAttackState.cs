using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyApplyForce;
    private bool endForce;
    private AttackInfoData attackInfoData;

    private List<Collider2D> HitEnemy;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        stateMachine.WantsToContinueCombo = false;
        alreadyApplyForce = false;
        endForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttakData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);

        AudioManager.Instance.PlaySFX("Sword" + comboIndex);

        HitEnemy = new List<Collider2D>();
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
            TryApplyForce();

            if (normalizedTime >= 0.2f && !endForce)
            {
                stateMachine.Player.ForceReceiver.Reset();
                endForce = true;
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
            if (!HitEnemy.Contains(enemyCollider))
            {
                Boss enemy = enemyCollider.GetComponent<Boss>();

                Vector3 enemyPosition = enemyCollider.transform.position + new Vector3(0, 0f, 0);

                if (Player.Instance.playerstat.CurSP < Player.Instance.playerstat.MaxSP)
                { 
                    BarEventManager.Instance.SPBarCall(Player.Instance.playerstat.CurSP, Player.Instance.playerstat.CurSP + 1);
                    Player.Instance.playerstat.AddSP(1);
                }

                if (enemy != null)
                {
                    enemy.TakeDamage(Player.Instance.playerstat.AttackPower);
                }
                else
                {
                    Monster monster = enemyCollider.GetComponent<Monster>();
                    monster.TakeDamage(Player.Instance.playerstat.AttackPower);
                }

                float randomAngle = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);

                ObjectPoolManager.Instance.GetFromPool(Player.Instance.slashPrefab, enemyPosition, randomRotation);
                HitEnemy.Add(enemyCollider);
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
        stateMachine.Player.ForceReceiver.AddForceAttack(forceDirection * attackInfoData.Force);
    }
}