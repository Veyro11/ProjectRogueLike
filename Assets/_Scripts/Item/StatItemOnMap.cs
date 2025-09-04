using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatItemOnMap : MonoBehaviour
{
    public ItemSO itemData;
    private bool playerInRange = false;
    private PlayerStatus playerStatus;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerStatus = other.GetComponent<PlayerStatus>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerStatus = null;
        }
    }

    private void Update()
    {
        if (playerInRange && playerStatus != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("현재 공격력: " + playerStatus.AttackPower);
            foreach (var mod in itemData.modifiers)
            {
                if (mod.statType == StatType.Attack)
                {
                    playerStatus.FixAttackPower((int)mod.value);
                    Debug.Log("아이템 획득 후 현재 공격력: " + playerStatus.AttackPower);
                }
            }
            gameObject.SetActive(false);
        }
    }
}
