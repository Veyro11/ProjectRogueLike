using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    public Image fadeImage; // 전체 화면 검은색 UI Image
    public float fadeDuration = 0.25f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            fadeImage.color = new Color(0, 0, 0, 0); // 초기 투명
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, endAlpha);
    }

    public void RequestTeleport(string mapName, Vector3 position)
    {
        StartCoroutine(TeleportCoroutine(mapName, position));
    }

    private IEnumerator TeleportCoroutine(string mapName, Vector3 position)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        MapManager.Instance.TransitionToMap(mapName, position);

        yield return new WaitForSecondsRealtime(0.5f);

        yield return StartCoroutine(Fade(1f, 0f));
    }
}
