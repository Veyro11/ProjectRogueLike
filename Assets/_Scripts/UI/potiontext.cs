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
        { return; }
        text.text = $"{(Player.Instance.playerstat.CurPotions == 0 ? "<color=#FF0000>"+Player.Instance.playerstat.CurPotions+"</color>" : Player.Instance.playerstat.CurPotions)} / {Player.Instance.playerstat.MaxPotions}";
    }

}