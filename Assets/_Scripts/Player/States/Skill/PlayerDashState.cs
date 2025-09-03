using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDashState : PlayerBaseState
{
    private float dashStartTime;
    private float lastEffectTime;
    private PlayerDashData dashData;

    private float effectDelay = 0.1f;

    private int playerLayer;
    private int playerDashLayer;

    public PlayerDashState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        dashData = stateMachine.Player.Data.DashData;
        playerLayer = LayerMask.NameToLayer("Player");
        playerDashLayer = LayerMask.NameToLayer("PlayerDash");
    }

    public override void Enter()
    {
        base.Enter();

        dashStartTime = Time.time;
        stateMachine.Player.Animator.SetBool(stateMachine.Player.AnimationData.DashParameterHash, true);

        Vector2 forceDirection = stateMachine.Player.transform.right * stateMachine.Player.transform.localScale.x;
        stateMachine.Player.ForceReceiver.AddForce(forceDirection * dashData.DashForce);

        stateMachine.Player.gameObject.layer = playerDashLayer;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.Animator.SetBool(stateMachine.Player.AnimationData.DashParameterHash, false);

        stateMachine.Player.gameObject.layer = playerLayer;
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= lastEffectTime + effectDelay)
        {
            Vector3 effectPosition = stateMachine.Player.transform.position;

            Quaternion effectRotation = stateMachine.Player.transform.localScale.x > 0 ?
                Quaternion.identity : Quaternion.Euler(0, 180, 0);

            ObjectPoolManager.Instance.GetFromPool(Player.Instance.dashPrefab, effectPosition, effectRotation);

            lastEffectTime = Time.time;
        }

        if (Time.time >= dashStartTime + dashData.DashDuration)
        {
            if (stateMachine.Player.IsGrounded())
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
    }
}