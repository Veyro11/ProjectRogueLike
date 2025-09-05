using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossStateMachine : StateMachine
{
    public Boss Enemy { get; }

    public EnemyGroundChecker enemyGroundChecker;
    public SpriteRenderer attackRenderer;
    public BoxCollider2D attackCollider;
    public Transform targetTransform;
    public Transform ownerTransform;
    public Vector3 spawnPosition;
    

    public BossIdleState IdleState { get; private set; }
    public BossChaseState ChaseState { get; private set; }
    public BossAttackState AttackState { get; private set; }
    public BossCoolTimeState CoolTimeState { get; private set; }
    public BossDieState DieState { get; private set; }
    public BossReturnState ReturnState { get; private set; }
    

    public BossStateMachine(Boss enemy)
    {
        Enemy = enemy;
        IdleState = new BossIdleState(this);
        ChaseState = new BossChaseState(this);
        AttackState = new BossAttackState(this);
        CoolTimeState = new BossCoolTimeState(this);
        DieState = new BossDieState(this);
        ReturnState = new BossReturnState(this);
        DieState = new BossDieState(this);

        targetTransform = enemy.target;
        ownerTransform = enemy.transform;
        spawnPosition = ownerTransform.position;
        attackRenderer = enemy.attackRenderer;

        attackCollider = enemy.transform.Find("AttackRange").GetComponent<BoxCollider2D>();
        enemyGroundChecker = enemy.gameObject.GetComponentInChildren<EnemyGroundChecker>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        currentState?.OntriggerEnter2D(collider);
    }
   
}
