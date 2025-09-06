using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    Slider slider;

    private void Start()
    {
        slider = transform.GetChild(2).GetChild(2).GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (Player.Instance == null)
        { return; }
        Player.Instance.PauseUser(false);
    }

    private void OnDisable()
    {
        Player.Instance.PauseUser(true);
    }

    public void BGControll()
    {
        slider = transform.GetChild(2).GetChild(2).GetComponent<Slider>();
        float intensity = slider.value;
        audioManager.SetBGMVolume(intensity);
    }

    public void SEControll()
    {
        slider = transform.GetChild(2).GetChild(3).GetComponent<Slider>();
        float intensity = slider.value;
        audioManager.SetSFXVolume(intensity);
    }

    public void exit()
    {
        gameObject.SetActive(false);
    }
}