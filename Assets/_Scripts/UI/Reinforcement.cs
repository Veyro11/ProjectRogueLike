using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ReinforcementCategory
{
    HP,
    ATK,
    Potion,
    SP,
    Special
}

public class Reinforcement : MonoBehaviour
{
    [SerializeField] PlayerSO _player;
    public int curSoul { get; set; }
    [Header("Reinforcement Config")]
    [SerializeField] Dictionary<ReinforcementCategory, int> ReinforcementCost;
    [SerializeField] public Dictionary<ReinforcementCategory, int> MaxReinforcableCount { get; set; }
    [HideInInspector] public Dictionary<ReinforcementCategory, int> curReinforcableCount { get; set; }

    private void Start()
    {
        curSoul = _player.MaxSouls;
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

    public bool PlusReinforce(ReinforcementCategory category)
    {
        if (MaxReinforcableCount == null)
            return false;
        if (ReinforcementCost == null)
            return false;

        if (MaxReinforcableCount.ContainsKey(category))
        {
            if (MaxReinforcableCount[category] <= curReinforcableCount[category])
            {
                Debug.Log($"이미 최대 강화상태입니다: type:{category}");
                return false;
            }
            if (curSoul < ReinforcementCost[category])
            {
                Debug.Log("영혼 양이 부족합니다.");
                return false;
            }
            curReinforcableCount[category]++;
            UpdatePlayerReinforcement(category);
            curSoul -= ReinforcementCost[category];
        }
        else
        {
            Debug.Log($"알 수 없는 카테고리가 호출되었습니다: type:{category}");
            return false;
        }
            return true;
    }
    public bool MinusReinforce(ReinforcementCategory category)
    {
        if (MaxReinforcableCount == null)
            return false;
        if (ReinforcementCost == null)
            return false;

        if (MaxReinforcableCount.ContainsKey(category))
        {
            if (0 >= curReinforcableCount[category])
            {
                Debug.Log($"강화가 되어있지 않습니다: type:{category}");
                return false;
            }
            curReinforcableCount[category]--;
            UpdatePlayerReinforcement(category);
        }
        else
        {
            Debug.Log($"알 수 없는 카테고리가 호출되었습니다: type:{category}");
            return false;
        }
        return true;
    }

    private void UpdatePlayerReinforcement(ReinforcementCategory category)
    {
        if (MaxReinforcableCount == null)
            return;
        if (ReinforcementCost == null)
            return;
        switch (category)
            {
            case ReinforcementCategory.HP:
                _player.FixMaxHealth(curReinforcableCount[category]);
                break;
            case ReinforcementCategory.Potion:
                _player.SetMaxPotions(curReinforcableCount[category]);
                break;
            case ReinforcementCategory.ATK:
                _player.FixAttackPower(curReinforcableCount[category]);
                break;
            case ReinforcementCategory.SP:
                _player.FixSPEfficiency(curReinforcableCount[category]);
                break;
            case ReinforcementCategory.Special:
                _player.SpecialUnlocked = curReinforcableCount[category] == 1;
                break;
            default:
                Debug.Log("알 수 없는 타입 입력됨.");
                break;
        }
    }
}