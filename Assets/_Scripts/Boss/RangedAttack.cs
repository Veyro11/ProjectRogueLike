using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RangedAttack : MonoBehaviour
{
    private Boss boss;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;

        Debug.Log($"Boss: {boss.name}, EnemyData: {boss.gameObject.GetComponent<Boss>().EnemyData}, Player: {Player.Instance}");

        if (layer != LayerMask.NameToLayer("Player")) return;

        Player.Instance.TakeDamage(boss.EnemyData.Attack);
    }

    public void InitBoss(Boss boss)
    {
        this.boss = boss;
    }
}
