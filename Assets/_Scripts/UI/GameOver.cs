using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameOver : MonoBehaviour
{
    [SerializeField] Transform VillageSpawnPoint;
    [SerializeField] SaveLoadManager SaveLoadManager;
    CanvasGroup canvasGroup;


    private void OnEnable()
    {
        if (Player.Instance == null)
        {
            Debug.Log("플레이어 인스턴스 눌리퍼런스 발생");
            return;
        }
        Player.Instance.PauseUser(true);
        fadeInOut(true);
    }

    public void Continue()
    {
        FadeManager.Instance.RequestTeleport("VillageMap", VillageSpawnPoint.position);
        StartCoroutine(fadeInOut(false));
        gameObject.SetActive(false);

        Player.Instance.Revive();
    }

    IEnumerator fadeInOut(bool mode)
    {
        if (mode)
        {
            for (float i = 0; i < 100; i++)
            {
                canvasGroup.alpha = i / 100f;
                yield return null;
            }
        }
        else
        {
            for (float i = 100; i >= 0; i--)
            {
                canvasGroup.alpha = i / 100f;
                yield return null;
            }
        }
    }

    public void QuitGame()
    {
        // Save Method Insert
        SaveLoadManager.SaveData();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
