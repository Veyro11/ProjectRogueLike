using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Enemy> enemies;
    public GameObject enemyPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = Player.Instance.transform.position + new Vector3(-5.1f, 0.3f, 0f);
        enemies.Add(Instantiate(enemyPrefab, spawnPos, Quaternion.identity).GetComponent<Enemy>());
    }
}
