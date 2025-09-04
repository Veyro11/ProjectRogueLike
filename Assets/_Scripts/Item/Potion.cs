using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int MaxPotions; // 최대 포션 수
    public int currentPotions;  // 보유 포션 수
    public float healAmount;  // 회복량

    private Player player;

    private void Awake()
    {
        player = Player.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsePotion();
        }
    }

    private void UsePotion()
    {
        if (currentPotions > 0)
        {
            player.Heal(healAmount);
            currentPotions--;
            Debug.Log($"포션 사용! 남은 포션: {currentPotions}");
        }
        else
        {
            Debug.Log("포션이 없습니다!");
        }
    }

    public void IncreaseMaxPotions(int amount)
    {
        MaxPotions += amount;
        Debug.Log($"최대 포션 개수 증가: {MaxPotions}");
    }
}
