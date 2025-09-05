using System.Collections.Generic;
using UnityEngine;

public class ReinforceManager : SingletonMono<ReinforceManager>
{
    //public static ReinforceManager Instance;
    [SerializeField] private Reinforcement reinforcement;
    [SerializeField] private ReinforcementTextEditor editor;
    private PlayerStatus _player;

    public List<ReinforcementCategory> category = new List<ReinforcementCategory>() {
        ReinforcementCategory.HP,
        ReinforcementCategory.Potion,
        ReinforcementCategory.ATK,
        ReinforcementCategory.SP,
        ReinforcementCategory.Special};

    protected override void Awake()
    {
        base.Awake();
        Debug.Log($"[Debug] Awake ReinforceManager ID: {this.GetInstanceID()}");
    }
    private void Start()
    {
        _player = Player.Instance.playerstat;

        Debug.Log($"[Debug] ReinforceManager instance: {this.GetInstanceID()}");
        Debug.Log($"[Debug] editor is null? {editor == null}");
        Debug.Log($"[Debug] reinforcement is null? {reinforcement == null}");
        if (editor == null)
        {
            Debug.LogError("Editor is null despite being assigned. Possibly wrong instance?");
        }
    }

    public void ManualReloadEdit(ReinforcementTextEditor edit)
    {
        this.editor = edit;
    }

    public void ManualReloadReinforcement(Reinforcement reinforce)
    {
        this.reinforcement = reinforce;
    }
    public void PlusReinforce(int value)
    {
        ReinforcementCategory category = ConvertInttoEnum(value);
        if (category == ReinforcementCategory.Error)
        {
            return;
        }
        if (reinforcement.MaxReinforcableCount == null)
            return;
        if (reinforcement.ReinforcementCost == null)
            return;

        if (reinforcement.MaxReinforcableCount.ContainsKey(category))
        {
            if (reinforcement.MaxReinforcableCount[category] <= reinforcement.curReinforcableCount[category])
            {
                editor.ChangePlusButtons(category, false);
                Debug.Log($"이미 최대 강화상태입니다: type:{category}");
                return;
            }
            if (_player.CurSouls < reinforcement.ReinforcementCost[category])
            {
                Debug.Log("영혼 양이 부족합니다.");
                return;
            }
            reinforcement.curReinforcableCount[category]++;
            UpdatePlayerReinforcement(category);
            _player.SetSoul(-reinforcement.ReinforcementCost[category]);
            editor.ChangeMinusButtons(category, true);
            updateUI(category);
        }
        else
        {
            Debug.Log($"알 수 없는 카테고리가 호출되었습니다: type:{category}");
            return;
        }
        return;
    }
    public void MinusReinforce(int value)
    {
        ReinforcementCategory category = ConvertInttoEnum(value);
        if (category == ReinforcementCategory.Error)
        {
            return;
        }
        if (reinforcement.MaxReinforcableCount == null)
            return;
        if (reinforcement.ReinforcementCost == null)
            return;

        if (reinforcement.MaxReinforcableCount.ContainsKey(category))
        {
            if (0 >= reinforcement.curReinforcableCount[category])
            {
                editor.ChangeMinusButtons(category, false);
                Debug.Log($"강화가 되어있지 않습니다: type:{category}");
                return;
            }
            reinforcement.curReinforcableCount[category]--;
            UpdatePlayerReinforcement(category);
            _player.SetSoul(reinforcement.ReinforcementCost[category]);
            editor.ChangePlusButtons(category, true);
            updateUI(category);
        }
        else
        {
            Debug.Log($"알 수 없는 카테고리가 호출되었습니다: type:{category}");
            return;
        }
        return;
    }


    private void UpdatePlayerReinforcement(ReinforcementCategory category)
    {
        if (reinforcement.MaxReinforcableCount == null)
            return;
        if (reinforcement.ReinforcementCost == null)
            return;
        switch (category)
        {
            case ReinforcementCategory.HP:
                _player.FixMaxHealth(reinforcement.curReinforcableCount[category] * 30);
                break;
            case ReinforcementCategory.Potion:
                _player.SetMaxPotions(reinforcement.curReinforcableCount[category]);
                break;
            case ReinforcementCategory.ATK:
                _player.FixAttackPower(reinforcement.curReinforcableCount[category] * 2);
                break;
            case ReinforcementCategory.SP:
                _player.FixSPEfficiency(reinforcement.curReinforcableCount[category] * 5);
                break;
            case ReinforcementCategory.Special:
                _player.SetUnlockTrigger(reinforcement.curReinforcableCount[category] == 1);
                break;
            default:
                Debug.Log("알 수 없는 타입 입력됨.");
                break;
        }
    }

    public void updateUI(ReinforcementCategory category)
    {
        if (editor == null)
        {
            Debug.Log("editor null");
            return;
        }
        editor.ChangeSouls();
        editor.ChangeNowStatus(category);
    }

    private ReinforcementCategory ConvertInttoEnum(int value)
    {
        switch (value)
        {
            case 0:
                return ReinforcementCategory.HP;
            case 1:
                return ReinforcementCategory.Potion;
            case 2:
                return ReinforcementCategory.ATK;
            case 3:
                return ReinforcementCategory.SP;
            case 4:
                return ReinforcementCategory.Special;
            default:
                Debug.Log("유효하지 않은 값 감지됨");
                return ReinforcementCategory.Error;
        }
    }


    public int GetCount(ReinforcementCategory category)
    {
        switch (category)
        {
            case ReinforcementCategory.HP:
                return reinforcement.curReinforcableCount[category];
            case ReinforcementCategory.Potion:
                return reinforcement.curReinforcableCount[category];
            case ReinforcementCategory.ATK:
                return reinforcement.curReinforcableCount[category];
            case ReinforcementCategory.SP:
                return reinforcement.curReinforcableCount[category];
            case ReinforcementCategory.Special:
                return reinforcement.curReinforcableCount[category];
            default:
                Debug.Log("알 수 없는 타입 입력됨.");
                return -1;
        }
    }

    public int GetMax(ReinforcementCategory category)
    {
        switch (category)
        {
            case ReinforcementCategory.HP:
                return reinforcement.MaxReinforcableCount[category];
            case ReinforcementCategory.Potion:
                return reinforcement.MaxReinforcableCount[category];
            case ReinforcementCategory.ATK:
                return reinforcement.MaxReinforcableCount[category];
            case ReinforcementCategory.SP:
                return reinforcement.MaxReinforcableCount[category];
            case ReinforcementCategory.Special:
                return reinforcement.MaxReinforcableCount[category];
            default:
                Debug.Log("알 수 없는 타입 입력됨.");
                return -1;
        }
    }

    public int getCost(ReinforcementCategory category)
    {
        switch (category)
        {
            case ReinforcementCategory.HP:
                return reinforcement.ReinforcementCost[category];
            case ReinforcementCategory.Potion:
                return reinforcement.ReinforcementCost[category];
            case ReinforcementCategory.ATK:
                return reinforcement.ReinforcementCost[category];
            case ReinforcementCategory.SP:
                return reinforcement.ReinforcementCost[category];
            case ReinforcementCategory.Special:
                return reinforcement.ReinforcementCost[category];
            default:
                Debug.Log("알 수 없는 타입 입력됨.");
                return -1;
        }
    }

    public void Refresh()
    {
        foreach (var i in category)
        {
            ReinforceManager.Instance.updateUI(i);
        }
    }    

    public bool editorTest()
    {
        if (editor == null)
        {
            return false;
        }
        return true;
    }

    public bool reinforcementTest()
    {
        if (reinforcement == null)
            { return false; }
        return true;
    }

    public void LoadReinforcementData(PlayerSaveData data)
    {
        reinforcement.curReinforcableCount[ReinforcementCategory.HP] = data.HPReinforcement;
        reinforcement.curReinforcableCount[ReinforcementCategory.Potion] = data.PotionReinforcement;
        reinforcement.curReinforcableCount[ReinforcementCategory.ATK] = data.ATKReinforcement;
        reinforcement.curReinforcableCount[ReinforcementCategory.SP] = data.SPReinforcement;
        reinforcement.curReinforcableCount[ReinforcementCategory.Special] = data.SpecialReinforcement;
    }

}
