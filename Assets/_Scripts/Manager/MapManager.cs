using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public GameObject villageMap;
    public GameObject firstStageMap;
    public GameObject secStageMap;
    // 필요하면 다른 맵들도 추가

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void TransitionToMap(string mapName, Vector3 playerPosition)
    {
        // 모든 맵 비활성화
        villageMap.SetActive(false);
        firstStageMap.SetActive(false);
        secStageMap.SetActive(false);
        // 대상 맵 활성화
        switch (mapName)
        {
            case "VillageMap":
                villageMap.SetActive(true);
                BarEventManager.Instance.HPBarCall(Player.Instance.playerstat.CurHealth, Player.Instance.playerstat.MaxHealth);
                BarEventManager.Instance.SetBossBar(false);
                Player.Instance.Heal(Player.Instance.playerstat.MaxHealth);
                break;
            case "FirstStageMap":
                firstStageMap.SetActive(true);
                break;
            case "SecStageMap":
                secStageMap.SetActive(true);
                break;
        }

        // 플레이어 위치 이동
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerPosition;

       
    }
}
