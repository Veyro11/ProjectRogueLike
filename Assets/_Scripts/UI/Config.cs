using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    public void BGControll(float intensity)
    {
        audioManager.bgmVolume = intensity;
    }

    public void SEControll(float intensity)
    {
        audioManager.sfxVolume = intensity;
    }

    public void exit()
    {
        gameObject.SetActive(false);
    }
}