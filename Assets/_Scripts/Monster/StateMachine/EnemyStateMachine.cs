using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public SpriteRenderer attackRenderer;
    public Transform targetTransform;
    public Transform ownerTransform;
    public Vector3 spawnPosition;

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyCoolTimeState CoolTimeState { get; private set; }
    public EnemyDieState DieState { get; private set; }
    public EnemyReturnState ReturnState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        CoolTimeState = new EnemyCoolTimeState(this);
        DieState = new EnemyDieState(this);
        ReturnState = new EnemyReturnState(this);
        
        targetTransform = enemy.target;
        ownerTransform = enemy.transform;
        spawnPosition = ownerTransform.position;
        attackRenderer = enemy.attackRenderer;
    }
}
