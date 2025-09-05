using UnityEngine;
using TMPro;

public class potiontext : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void updatePotionText()
    {
        if (text == null)
        { Debug.Log("텍스트 이즈 눌"); return; }
        text.text = $"{Player.Instance.playerstat.CurPotions} / {Player.Instance.playerstat.MaxPotions}"; 
    }

}