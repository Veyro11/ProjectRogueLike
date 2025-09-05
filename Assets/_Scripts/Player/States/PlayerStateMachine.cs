using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerClimbingState ClimbingState { get; private set; }
    public Vector2 LedgePosition { get; set; }

    public float JumpForce { get; set; }
    public bool IsRunning { get; set; }
    public int ComboIndex { get; set; }
    public float LastDashTime { get; set; }
    public float DashCooldown { get; private set; } = 2f;
    public PlayerDieState DieState { get; private set; }

    public bool WantsToContinueCombo { get; set; }

    public Transform MainCameraTransform { get; set; }
    public PlayerComboAttackState ComboAttackState { get; }

    public PlayerSpecialAttackState SpecialAttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        AirState = new PlayerAirState(this);

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);

        //MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;

        DashState = new PlayerDashState(this);
        ClimbingState = new PlayerClimbingState(this);
        ComboAttackState = new PlayerComboAttackState(this);
        DieState = new PlayerDieState(this);
        SpecialAttackState = new PlayerSpecialAttackState(this);

        LastDashTime = -DashCooldown;

    }
}