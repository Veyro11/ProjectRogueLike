using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WannaExit : MonoBehaviour
{
    [SerializeField] SaveLoadManager SLmanager;
    CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        fadeInOut(true);
        Player.Instance.PauseUser(false);
    }
    public void quitWindow()
    {
        Player.Instance.PauseUser(true);
        gameObject.SetActive(false);
    }

    public void quitGame()
    {
        SLmanager.SaveData();


#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
}