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
    public int CurSouls { get; private set; }

    public int CurPotions { get; private set; }
    public int MaxPotions { get; private set; }
    public int MaxSP { get; private set; }
    public int CurSP { get; private set; }
    public int SPEfficiency { get; private set; }
    public bool SpecialUnlocked { get; private set; }
    public float MoveSpeed { get; private set; }
    public float InvincibilityTime { get; private set; }

    private float baseMaxHealth;
    private float baseAttackPower;
    private int baseMaxPotions;


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

        baseMaxHealth = MaxHealth;
        baseAttackPower = AttackPower;
        baseMaxPotions = MaxPotions;
    }

    public void AddSP(int amount)
    {
        CurSP += amount;
        Debug.Log($"SP: {CurSP}");
        if (CurSP >= MaxSP)
        {
            CurSP = MaxSP;
        }
    }

    public void SetPotion(int amount)
        { CurPotions += amount; }
    public void SetSoul(int amount)
    { CurSouls += amount; }
    public void FixMaxHealth(int amount)
    { MaxHealth = 100f + amount * 30f; }

    public void FixAttackPower(int amount)
    { AttackPower += amount; }


    public void SetMaxSouls(int amount)
    { 
        MaxSouls = amount;
        // update curSouls
        CurSouls = amount;
        foreach (var i in ReinforceManager.Instance.category)
        {
            CurSouls -= ReinforceManager.Instance.getCost(i) * ReinforceManager.Instance.GetCount(i);
        }
    }

    public void SetMaxPotions(int amount)
    { MaxPotions = amount; }

    public void FixSPEfficiency(int amount)
    { SPEfficiency = 1 + amount; }

    public void SetUnlockTrigger(bool trigger)
    {
        SpecialUnlocked = trigger;
    }
    public void SetMoveSpeed(float amount)
    {
        MoveSpeed = amount;
    }

    public void UsePotion()
    {
        if (CurPotions > 0)
        {
            CurPotions--;
            Player.Instance.Heal(30f);
            Debug.Log($"포션 {CurPotions}");
            UIManager.Instance.UpdatePotion();
        }
    }
        public void ResetStatsToBase()
    {
        CurHealth = baseMaxHealth;
        AttackPower = baseAttackPower;
        MaxPotions = baseMaxPotions;
    }

    public void Load(PlayerSaveData data)
    {
        MaxHealth = data.MaxHealth;
        Player.Instance.currentHealth = data.CurHealth;
        AttackPower = data.AttackPower;
        MaxSouls = data.MaxSouls;
        MaxPotions = data.MaxPotions;
        MaxSP = data.MaxSP;
        CurSP = data.curSP;
        InvincibilityTime = data.InvincibilityTime;
        SpecialUnlocked = data.SpecialUnlocked;
    }

}
