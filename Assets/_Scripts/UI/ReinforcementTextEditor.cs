using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ReinforcementTextEditor : MonoBehaviour
{
    [SerializeField] Transform motherTransform;
    [SerializeField] PlayerStatus _player;

    private void Start()
    {
        if (ReinforceManager.Instance!=null && !ReinforceManager.Instance.editorTest())
        {
            ReinforceManager.Instance.ManualReloadEdit(this);
        }
    }

    public void ChangeSouls()
    {
        TMP_Text text = motherTransform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        text.text = $"{Player.Instance.playerstat.CurSouls} / {Player.Instance.playerstat.MaxSouls}";
    }
    public void ChangeNowStatus(ReinforcementCategory category)
    {
        Transform nowstatus = motherTransform.GetChild(4);
        TMP_Text text;
        switch (category)
        {
            case ReinforcementCategory.HP:
                text = nowstatus.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
                text.text = $"+{ReinforceManager.Instance.GetCount(category) * 30} / {ReinforceManager.Instance.GetMax(category) * 30}";
                break;
            case ReinforcementCategory.Potion:
                text = nowstatus.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
                text.text = $"+{ReinforceManager.Instance.GetCount(category)} / {ReinforceManager.Instance.GetMax(category)}";
                break;
            case ReinforcementCategory.ATK:
                text = nowstatus.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
                text.text = $"+{ReinforceManager.Instance.GetCount(category) * 2} / {ReinforceManager.Instance.GetMax(category) * 2}";
                break;
            case ReinforcementCategory.SP:
                text = nowstatus.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
                text.text = $"+{ReinforceManager.Instance.GetCount(category) * 5}% / {ReinforceManager.Instance.GetMax(category) * 5}%";
                break;
            case ReinforcementCategory.Special:
                text = nowstatus.GetChild(4).GetChild(0).GetComponent<TMP_Text>();
                text.text = ReinforceManager.Instance.GetCount(category) == 1 ? "UNLOCKED" : "LOCKED";
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

