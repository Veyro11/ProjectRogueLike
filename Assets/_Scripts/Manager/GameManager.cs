using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Main;
    public GameObject camera;

    public Animator titleAnimator;


    private void Start()
    {
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

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);

        Player.Instance.PauseUser(true);

        AudioManager.Instance.PlayBGM("Battle");
        UIManager.Instance.SetHUD();
        UIManager.Instance.readyInput();
    }

}