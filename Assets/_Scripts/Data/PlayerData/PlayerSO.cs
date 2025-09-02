using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}

[Serializable]
public class PlayerAirData
{
    [field: Header("JumpData")]
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 5f;

}

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }
}

[Serializable]
public class PlayerDashData
{
    [field: SerializeField][field: Range(0f, 50f)] public float DashForce { get; private set; } = 20f;
    [field: SerializeField][field: Range(0f, 1f)] public float DashDuration { get; private set; } = 0.2f;
}


[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]

public class PlayerSO : ScriptableObject
{
    [Tooltip("체력")]
    public float MaxHealth { get; private set; } = 100f;

    [Tooltip("공격력")]
    public float AttackPower { get; private set; } = 10f;

    [Tooltip("공격속도")]
    public float AttackSpeed { get; private set; } = 0f;

    [HideInInspector] public int MaxSouls { get; private set; }

    [HideInInspector] public int MaxPotions { get; private set; }

    [Tooltip("스킬 포인트")]
    public int MaxSP { get; private set; } = 20;
    [Tooltip("현재 스킬 포인트")]
    public int curSP = 0;

    [HideInInspector] public int SPEfficiency { get; private set; } = 1;

    [HideInInspector] public bool SpecialUnlocked = false;

    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttakData { get; private set; }
    [field: SerializeField] public PlayerDashData DashData { get; private set; }

    public void FixMaxHealth(int amount)
    { MaxHealth = 100f + amount*30f; }

    public void FixAttackPower(int amount)
    { AttackPower += 10f + amount*5f; }

    public void FixAttackSpeed(int amount)
    { AttackSpeed += 1 - amount*0.1f; }

    public void SetMaxSouls(int amount)
    { MaxSouls = amount; }

    public void SetMaxPotions(int amount)
    { MaxPotions = amount; }

    public void FixSPEfficiency(int amount)
    { SPEfficiency = 1 + amount; }

}
