using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }

    public EnemyGroundChecker monsterGroundChecker;
    public SpriteRenderer attackRenderer;
    public BoxCollider2D attackCollider;
    public Transform targetTransform;
    public Transform ownerTransform;
    public Vector3 spawnPosition;

    public MonsterIdleState IdleState { get; private set; }
    public MonsterChaseState ChaseState { get; private set; }
    public MonsterAttackState AttackState { get; private set; }
    public MonsterCoolTimeState CoolTimeState { get; private set; }
    public MonsterDieState DieState { get; private set; }
    public MonsterReTurnState ReturnState { get; private set; }

    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        IdleState = new MonsterIdleState(this);
        ChaseState = new MonsterChaseState(this);
        AttackState = new MonsterAttackState(this);
        CoolTimeState = new MonsterCoolTimeState(this);
        DieState = new MonsterDieState(this);
        ReturnState = new MonsterReTurnState(this);

        targetTransform = monster.target;
        ownerTransform = monster.transform;
        spawnPosition = ownerTransform.position;
        attackRenderer = monster.attackRenderer;

        attackCollider = monster.transform.Find("AttackRange").GetComponent<BoxCollider2D>();
        monsterGroundChecker = monster.gameObject.GetComponentInChildren<EnemyGroundChecker>();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        currentState?.OntriggerEnter2D(collider);
    }
}
