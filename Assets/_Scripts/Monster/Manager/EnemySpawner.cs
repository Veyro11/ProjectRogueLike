using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private void Start()
    {
        SpawnEnemy();
    }

    // 보스 생성 메서드, 맵에따라 위치 고정값으로 넣어줄까 생각 중 입니다.
    public void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1.3f, 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity).GetComponent<Enemy>();
    }
}
