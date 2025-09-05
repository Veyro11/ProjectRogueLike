using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCheck : MonoBehaviour
{
    [SerializeField] private GameObject bossRoomWall; // 벽 오브젝트
    [SerializeField] private Collider2D bossRoomTrigger; // 보스방 진입 트리거

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowWall();
        }
    }

    public void ShowWall()
    {
        if (bossRoomWall != null)
        {
            bossRoomWall.SetActive(true);
        }
    }

    public void HideWall()
    {
        bossRoomWall.SetActive(false);
        Debug.Log("bossRoomWall.SetActive(false) called");
    }

    // 보스가 죽었을 때 이 메서드를 호출
    public void OnBossDefeated()
    {
        Debug.Log("OnBossDefeated called, hiding wall...");
        HideWall();
    }
}
