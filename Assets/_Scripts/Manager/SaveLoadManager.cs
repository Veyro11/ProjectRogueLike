using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    public float MaxHealth;
    public float CurHealth;
    public float AttackPower;

    public int MaxSouls;
    public int MaxPotions;
    public int MaxSP;
    public int curSP;
    public int InvincibilityTime;
    public bool SpecialUnlocked;

    public float PlayerX;
    public float PlayerY;

    public float BgmVolume;
    public float SFXVolume;
}

public class SaveLoadManager : MonoBehaviour
{
    private Player player;

    public PlayerStatus playerstat;

    private string savePath;
    private string saveFileName = "playerdata.json";

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
    }

    private void Start()
    {
        player = Player.Instance;
        PlayerStatus playerstat = player.playerstat;
}

    public void SaveData()
    {
        PlayerSaveData saveData = new PlayerSaveData();
        saveData.MaxHealth = playerstat.MaxHealth;
        saveData.AttackPower = playerstat.AttackPower;
        saveData.MaxSP = playerstat.MaxSP;
        saveData.MaxSouls = playerstat.MaxSouls;
        saveData.MaxPotions = playerstat.MaxPotions;
        saveData.CurHealth = player.currentHealth;
        saveData.SpecialUnlocked = playerstat.SpecialUnlocked;

        saveData.BgmVolume = AudioManager.Instance.bgmVolume;
        saveData.SFXVolume = AudioManager.Instance.sfxVolume;

        saveData.PlayerX = 51.70639f;
        saveData.PlayerY = 0.5607741f;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"저장됨 {savePath}");
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerSaveData loadData = JsonUtility.FromJson<PlayerSaveData>(json);

            playerstat.Load(loadData);

            player.transform.position = new Vector3(loadData.PlayerX, loadData.PlayerY, player.transform.position.z);

            AudioManager.Instance.bgmVolume = loadData.BgmVolume;
            AudioManager.Instance.sfxVolume = loadData.SFXVolume;



            Debug.Log($"불러옴 {savePath}");
        }
        else
        {
            Debug.Log("파일없음.");
        }
    }
}