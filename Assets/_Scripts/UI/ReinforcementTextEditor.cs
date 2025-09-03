using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ReinforcementTextEditor : MonoBehaviour
{
    [SerializeField] Transform motherTransform;
    [SerializeField] PlayerStatus _player;
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
                text.text = $"+{reinforcement.curReinforcableCount[category] * 30} / {reinforcement.MaxReinforcableCount[category] * 30}";
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
                Debug.Log("알 수 없는 카테고리 타입");
                break;
        }
    }

    public void ChangePlusButtons(ReinforcementCategory category, bool? status = null)
    {
        Transform nowstatus = motherTransform.GetChild(5);
        Button button;
        switch (category)
        {
            case ReinforcementCategory.HP:
                button = nowstatus.GetChild(0).GetComponent<Button>();
                break;
            case ReinforcementCategory.Potion:
                button = nowstatus.GetChild(1).GetComponent<Button>();
                break;
            case ReinforcementCategory.ATK:
                button = nowstatus.GetChild(2).GetComponent<Button>();
                break;
            case ReinforcementCategory.SP:
                button = nowstatus.GetChild(3).GetComponent<Button>();
                break;
            case ReinforcementCategory.Special:
                button = nowstatus.GetChild(4).GetComponent<Button>();
                break;
            default:
                Debug.Log("해당되지 않는 코드 입력됨");
                return;
        }
        if (status != null)
        {
            button.interactable = (bool)status;
            return;
        }
        button.interactable = !button.interactable;
    }
    public void ChangeMinusButtons(ReinforcementCategory category, bool? status = null)
    {
        Transform nowstatus = motherTransform.GetChild(6);
        Button button;
        switch (category)
        {
            case ReinforcementCategory.HP:
                button = nowstatus.GetChild(0).GetComponent<Button>();
                break;
            case ReinforcementCategory.Potion:
                button = nowstatus.GetChild(1).GetComponent<Button>();
                break;
            case ReinforcementCategory.ATK:
                button = nowstatus.GetChild(2).GetComponent<Button>();
                break;
            case ReinforcementCategory.SP:
                button = nowstatus.GetChild(3).GetComponent<Button>();
                break;
            case ReinforcementCategory.Special:
                button = nowstatus.GetChild(4).GetComponent<Button>();
                break;
            default:
                Debug.Log("해당되지 않는 코드 입력됨");
                return;
        }
        if (status != null)
        {
            button.interactable = (bool)status;
            return;
        }
        button.interactable = !button.interactable;
    }

}

