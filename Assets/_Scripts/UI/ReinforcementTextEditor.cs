using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ReinforcementTextEditor : MonoBehaviour
{
    [SerializeField] Transform motherTransform;
    [SerializeField] PlayerSO _player;
    Reinforcement reinforcement;

    private void Start()
    {
        reinforcement = GetComponent<Reinforcement>();
    }
    public void ChangeSouls()
    {
        Text text = motherTransform.GetChild(1).GetChild(0).GetComponent<Text>();
        text.text = $"{reinforcement.curSoul} / {_player.MaxSouls}";
    }
    public void ChangeNowStatus(ReinforcementCategory category)
    {
        Transform nowstatus = motherTransform.GetChild(4);
        Text text;
        switch (category)
        {
            case ReinforcementCategory.HP:
                text = nowstatus.GetChild(0).GetChild(0).GetComponent<Text>();
                text.text = $"+{reinforcement.curReinforcableCount[category]*30} / {reinforcement.MaxReinforcableCount[category]*30}";
                break;
            case ReinforcementCategory.Potion:
                text = nowstatus.GetChild(1).GetChild(0).GetComponent<Text>();
                text.text = $"+{reinforcement.curReinforcableCount[category]} / {reinforcement.MaxReinforcableCount[category]}";
                break;
            case ReinforcementCategory.ATK:
                text = nowstatus.GetChild(2).GetChild(0).GetComponent<Text>();
                text.text = $"+{reinforcement.curReinforcableCount[category] * 10} / {reinforcement.MaxReinforcableCount[category] * 10}";
                break;
            case ReinforcementCategory.SP:
                text = nowstatus.GetChild(3).GetChild(0).GetComponent<Text>();
                text.text = $"+{reinforcement.curReinforcableCount[category] * 10}% / {reinforcement.MaxReinforcableCount[category] * 10}%";
                break;
            case ReinforcementCategory.Special:
                text = nowstatus.GetChild(4).GetChild(0).GetComponent<Text>();
                text.text = reinforcement.curReinforcableCount[category] == 1 ? "UNLOCKED" : "LOCKED";
                break;
            default:
                break;
        }
    }

    
}