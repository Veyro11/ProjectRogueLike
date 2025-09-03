using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    public float CurHealth { get; private set; }
    public float AttackPower { get; private set; }
    public float AttackSpeed { get; private set; }
    public int MaxSouls { get; private set; }
    public int MaxPotions { get; private set; }
    public int MaxSP { get; private set; }
    public int CurSP { get; private set; }
    public int SPEfficiency { get; private set; }
    public bool SpecialUnlocked { get; private set; }


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        PlayerSO Data = Player.Instance.Data;

        MaxHealth = Data.MaxHealth;
        CurHealth = MaxHealth;
        AttackPower = Data.AttackPower;
        AttackSpeed = Data.AttackSpeed;
        MaxSouls = Data.MaxSouls;
        MaxPotions = Data.MaxPotions;
        MaxSP = Data.MaxSP;
        CurSP = 0;
        SPEfficiency = Data.SPEfficiency;
        SpecialUnlocked = false;
    }


    public void FixMaxHealth(int amount)
    { MaxHealth = 100f + amount * 30f; }

    public void FixAttackPower(int amount)
    { AttackPower += 10f + amount * 5f; }

    public void FixAttackSpeed(int amount)
    { AttackSpeed += amount * 0.1f; }

    public void SetMaxSouls(int amount)
    { MaxSouls = amount; }

    public void SetMaxPotions(int amount)
    { MaxPotions = amount; }

    public void FixSPEfficiency(int amount)
    { SPEfficiency = 1 + amount; }

    public void SetUnlockTrigger(bool trigger)
    {
        SpecialUnlocked = trigger;
    }
    public void Load()
    {

    }
}
