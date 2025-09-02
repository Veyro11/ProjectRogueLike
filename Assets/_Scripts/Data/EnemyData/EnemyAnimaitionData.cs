using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimaitionData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string Attack_1_ParameterName = "Attack_1";
    [SerializeField] private string Attack_2_ParameterName = "Attack_2";
    [SerializeField] private string AttackReadyParameterName = "AttackReady";

    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int Attack_1_ParameterHash { get; private set; }
    public int Attack_2_ParameterHash { get; private set; }
    public int AttackReadyParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        Attack_1_ParameterHash = Animator.StringToHash(Attack_1_ParameterName);
        Attack_2_ParameterHash = Animator.StringToHash(Attack_2_ParameterName);
        AttackReadyParameterHash = Animator.StringToHash(AttackReadyParameterName);
    }
}
