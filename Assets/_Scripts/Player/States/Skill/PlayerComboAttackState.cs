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
        int damage = 1;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackRange.position, attackSize, 0f, monsterLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            if (!HitEnemy.Contains(enemyCollider))
            {
                MonsterHealth monster = enemyCollider.GetComponent<MonsterHealth>();
                Debug.Log("데미지");
                monster.TakeDamage(damage);

                Vector3 enemyPosition = enemyCollider.transform.position + new Vector3(0, -0.5f, 0);

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
        stateMachine.Player.ForceReceiver.AddForce(forceDirection * attackInfoData.Force);
    }
}