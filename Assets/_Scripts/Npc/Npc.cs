using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject Icon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Icon.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Icon.SetActive(false);
        }
    }

    //public void EnforceUiopen() // 업그레이드 UI 열기
    //{
    //    upgradeUI.SetActive(true);          // UI 활성화
    //    Time.timeScale = 0f;                // 게임 시간 멈춤
    //    isUpgradeUIActive = true;
    //    Cursor.visible = true;              // 커서 보이기 (필요시)
    //    Cursor.lockState = CursorLockMode.None;
    //}
}
