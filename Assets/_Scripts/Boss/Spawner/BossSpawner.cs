using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//보스를 스폰해주는 클래스 입니다
public class BossSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject currentPrefab;
    private Boss currentBoss;

    private void OnEnable()
    {
        SpawnEnemy();
    }

    private void OnDisable()
    {
        Reset();
    }

    // 보스 생성 메서드
    public Boss SpawnEnemy()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1.3f, 0f);
        currentPrefab = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        currentBoss = currentPrefab.GetComponent<Boss>();
        return currentBoss;
    }

    // 맵 초기화시 생성된 보스도 삭제하는 메서드
    public void Reset()
    {
        if (currentPrefab != null)
            Destroy(currentPrefab);
        currentBoss = null;
    }

    public Boss GetCurrentBoss()
    {
        return currentBoss;
    }
}
