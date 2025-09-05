using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Main;
    public GameObject camera;

    public Animator titleAnimator;

    public Button loadButton;

    public SaveLoadManager saveLoadManager;

    private string savePath;

    private void Awake()
    {
        saveLoadManager = GetComponentInChildren<SaveLoadManager>();

        savePath = System.IO.Path.Combine(Application.persistentDataPath, "playerdata.json");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    IEnumerator Start()
    {
        AudioManager.Instance.StopBGM();


        if (System.IO.File.Exists(savePath))
        {
            loadButton.interactable = true;
            Debug.Log(Application.persistentDataPath);
        }
        else
        {
            loadButton.interactable = false;
        }

        yield return new WaitForSeconds(1.0f);

        AudioManager.Instance.PlayBGM("Title");
    }

    public void EnableMainCamera()
    {
        Main.SetActive(true);
        camera.SetActive(false);
    }

    public void StartGame()
    {
        titleAnimator.SetBool("Start", true);
        StartCoroutine(Delay());
    }

    public void LoadGame()
    {
        titleAnimator.SetBool("Start", true);
        StartCoroutine(Delay());

        saveLoadManager.LoadData();
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);

        Player.Instance.PauseUser(true);

        AudioManager.Instance.PlayBGM("Battle");
        if (UIManager.Instance == null) {
            Debug.Log("유아이매니저 눌참조남...? 왜?");
        }
        UIManager.Instance.DebugUIS();
        UIManager.Instance.SetHUD();
        UIManager.Instance.readyInput();
    }

}