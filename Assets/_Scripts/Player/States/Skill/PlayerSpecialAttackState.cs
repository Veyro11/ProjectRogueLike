using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttackState : PlayerBaseState
{
    private float chargeDuration = 0.5f;
    private float slowMotionScale = 0.1f;
    private float slashDistance = 7f;
    private float slashSpeed = 80f;
    private float recoveryDuration = 0.5f;
    private float attackHeight = 3f;

    private string wallLayer = "Ground";
    private float wallset = 0.5f;

    private Coroutine specialAttackCoroutine;

    public PlayerSpecialAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.MovementSpeedModifier = 0f;

        stateMachine.Player.playerstat.AddSP(-stateMachine.Player.playerstat.MaxSP);
        BarEventManager.Instance.SPBarCall(stateMachine.Player.playerstat.MaxSP, 0);

        specialAttackCoroutine = stateMachine.Player.StartCoroutine(SpecialAttackSequence());
    }

    public override void Exit()
    {
        base.Exit();
        if (specialAttackCoroutine != null)
        {
            stateMachine.Player.StopCoroutine(specialAttackCoroutine);
        }
        Time.timeScale = 1.0f;
    }

    public override void Update() { }
    public override void PhysicsUpdate() { }

    private IEnumerator SpecialAttackSequence()
    {
        Transform playerTransform = stateMachine.Player.transform;

        Time.timeScale = slowMotionScale;
        stateMachine.Player.Animator.Play("Special_Charge");
        yield return new WaitForSecondsRealtime(chargeDuration);

        Time.timeScale = 1.0f;

        Vector2 startPosition = playerTransform.position;
        float direction = playerTransform.localScale.x;
        Vector2 endPosition = startPosition + new Vector2(slashDistance * direction, 0);

        int layerMask = LayerMask.GetMask(wallLayer);
        RaycastHit2D hit = Physics2D.Raycast(startPosition, new Vector2(direction, 0), slashDistance, layerMask);
        if (hit.collider != null)
        {
            endPosition = hit.point - new Vector2(wallset * direction, 0);
        }

        stateMachine.Player.Animator.Play("Special_Slash");

        while (Vector2.Distance(playerTransform.position, endPosition) > 0.01f)
        {
            playerTransform.position = Vector2.MoveTowards(playerTransform.position, endPosition, slashSpeed * Time.deltaTime);
            yield return null;
        }
        playerTransform.position = endPosition;

        DealDamageInPath(startPosition, playerTransform.position);

        yield return new WaitForSeconds(recoveryDuration);

        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void DealDamageInPath(Vector2 start, Vector2 end)
    {
        float pathLength = Vector2.Distance(start, end);
        if (pathLength <= 0) return;

        Vector2 pathCenter = (start + end) / 2;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(pathCenter, new Vector2(pathLength, attackHeight), 0, stateMachine.Player.MonsterLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            float specialDamage = stateMachine.Player.playerstat.AttackPower * 5f;

            Monster monster = enemyCollider.GetComponent<Monster>();
            if (monster != null) monster.TakeDamage(specialDamage);

            Boss boss = enemyCollider.GetComponent<Boss>();
            if (boss != null) boss.TakeDamage(specialDamage);
        }
    }
}