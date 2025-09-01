using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    public float timer;
    public int attackTime = 2;
    public int attackDIstance_2m = 2;
    public int attackDIstance_4m = 4;
    public int trackingDistance = 5;
    public int missingDistance = 8;
    private Vector3 lookDirection;

    public EnemyBaseState(EnemyStateMachine stateMachine)
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
        //stateMachine.Enemy.EnemyData.HP -= 1; //죽음 테스트
    }

    public virtual void LookPlayer()
    {
        lookDirection = stateMachine.targetTransform.position - stateMachine.Enemy.transform.position;

        if (lookDirection.x > 0)
        {
            // 오른쪽을 바라보게
            stateMachine.Enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 왼쪽을 바라보게
            stateMachine.Enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    protected void DieCheck()
    {
        if (stateMachine.Enemy.EnemyData.HP != 0) return;

        stateMachine.ChangeState(stateMachine.DieState);
    }
}
