using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor;


public enum ReinforcementCategory
{
    Error,
    HP,
    ATK,
    Potion,
    SP,
    Special
}

public class Reinforcement : MonoBehaviour
{
    [SerializeField] PlayerStatus _player;

    [SerializeField] public Dictionary<ReinforcementCategory, int> ReinforcementCost { get; set; }
    [SerializeField] public Dictionary<ReinforcementCategory, int> MaxReinforcableCount { get; set; }
    [HideInInspector] public Dictionary<ReinforcementCategory, int> curReinforcableCount { get; set; }

    private void Awake()
    {
        // 세이브/로드 필요 시 해당 코드 수정요망
        curReinforcableCount = new Dictionary<ReinforcementCategory, int>()
        {
            {ReinforcementCategory.HP, 0 },
            {ReinforcementCategory.Potion, 0 },
            {ReinforcementCategory.ATK, 0 },
            {ReinforcementCategory.SP, 0 },
            {ReinforcementCategory.Special, 0 }
        };


        if (MaxReinforcableCount == null || MaxReinforcableCount.Count < 5)
        {
            MaxReinforcableCount = new Dictionary<ReinforcementCategory, int>()
            {
                { ReinforcementCategory.HP, 5 },
                { ReinforcementCategory.Potion, 3 },
                { ReinforcementCategory.ATK, 5 },
                { ReinforcementCategory.SP, 5 },
                { ReinforcementCategory.Special, 1 }
            };
        }
        if (ReinforcementCost == null || ReinforcementCost.Count < 5)
        {
            ReinforcementCost = new Dictionary<ReinforcementCategory, int>()
            {
                { ReinforcementCategory.HP, 2 },
                { ReinforcementCategory.Potion, 2 },
                { ReinforcementCategory.ATK, 2 },
                { ReinforcementCategory.SP, 2 },
                { ReinforcementCategory.Special, 5 }
            };
        }
    }
    private void Start()
    {
        if (ReinforceManager.Instance != null && !ReinforceManager.Instance.reinforcementTest())
        {
            ReinforceManager.Instance.ManualReloadReinforcement(this);
        }
    }
    private void OnEnable()
    {
        Player.Instance.PauseUser(false);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        Player.Instance.PauseUser(true);
    }

}