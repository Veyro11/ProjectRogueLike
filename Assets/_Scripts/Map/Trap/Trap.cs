using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool isPlayerInTrap = false;
    private Coroutine damageCoroutine;
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            isPlayerInTrap = true;
            damageCoroutine = StartCoroutine(GiveDamage(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            isPlayerInTrap = false;
            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator GiveDamage(Collider2D player)
    {
        while (isPlayerInTrap)
        {
            Player.Instance.TakeDamage(damage);
            Debug.Log($"Player took {damage} damage from trap.");
            yield return new WaitForSeconds(1f);
        }
    }
}
