using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

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
    }

    protected void LookPlayer()
    {
        lookDirection = stateMachine.targetTransform.position - stateMachine.Enemy.transform.position;

        if (lookDirection.x > 0)
        {
            Debug.Log("오른쪽");
            // 오른쪽을 바라보게
            stateMachine.Enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.Log("왼쪽");
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
}
