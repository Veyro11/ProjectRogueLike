using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;

    public float timer;
    public int attackTime = 2;
    public int attackDIstance_2m = 2;
    public int attackDIstance_4m = 4;
    public int trackingDistance = 5;
    public int missingDistance = 8;
    private Vector3 lookDirection;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        LookPlayer();
        DieCheck();

    }

    public virtual void OntriggerEnter2D(Collider2D collision)
    {
    }

    public virtual void LookPlayer()
    {
        lookDirection = stateMachine.targetTransform.position - stateMachine.Monster.transform.position;

        if (lookDirection.x > 0)
        {
            // 오른쪽을 바라보게
            stateMachine.Monster.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 왼쪽을 바라보게
            stateMachine.Monster.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
    }

    protected void DieCheck()
    {
        if (stateMachine.Monster.MonsterData.HP > 0) return;

        stateMachine.ChangeState(stateMachine.DieState);
    }
}
