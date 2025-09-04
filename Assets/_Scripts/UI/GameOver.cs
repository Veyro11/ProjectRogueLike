using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    [SerializeField] Transform VillageSpawnPoint;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        fadeInOut(true);
    }

    public void Continue()
    {
        FadeManager.Instance.RequestTeleport("VillageMap", VillageSpawnPoint.GetComponent<Vector3>());
        fadeInOut(false);
        gameObject.SetActive(false);
    }

    IEnumerator fadeInOut(bool mode)
    {
        if (mode)
        {
            for (int i = 0; i < 100; i++)
            {
                canvasGroup.alpha = i / 100f;
                yield return null;
            }
        }
        else
        {
            for (int i = 100; i >= 0; i--)
            {
                canvasGroup.alpha = i / 100f;
                yield return null;
            }
        }
    }

}
