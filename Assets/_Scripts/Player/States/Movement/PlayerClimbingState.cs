using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingState : PlayerBaseState
{
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private Vector2 setPosition;

    private float climbDuration = 0.7f;
    private float climbStartTime;

    private Transform spriteTransform;

    public PlayerClimbingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ClimbingParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);

        stateMachine.Player.Rb.isKinematic = true;



        startPosition = stateMachine.Player.transform.position;
        targetPosition = stateMachine.LedgePosition;

        setPosition = new Vector2(targetPosition.x, targetPosition.y - 0.75f);
        
        climbStartTime = Time.time;

        spriteTransform = stateMachine.Player.SpriteTransform;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ClimbingParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
        

        spriteTransform.position = stateMachine.LedgePosition;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.Rb.isKinematic = false;
    }

    public override void Update()
    {
        base.Update();

        float progress = (Time.time - climbStartTime) / climbDuration;

        stateMachine.Player.transform.position = Vector2.Lerp(startPosition, targetPosition, progress);
        spriteTransform.position = setPosition;

        if (progress >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
    }
}