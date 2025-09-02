using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool isPlayerInTrap = false;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrap = true;
            damageCoroutine = StartCoroutine(GiveDamage(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrap = false;
            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator GiveDamage(Collider player)
    {
        while (isPlayerInTrap)
        {
            //player.GetComponent<StatusController>().DecreaseHP(damage); 플레이어 체력가져오기
            yield return new WaitForSeconds(1f);
        }
    }
}
