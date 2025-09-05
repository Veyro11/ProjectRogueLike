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
            Player.Instance.TakeDamage(damage);
        }
    }

}
