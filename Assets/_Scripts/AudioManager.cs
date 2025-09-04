using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clip;

    private Dictionary<string, AudioClip> audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

        clip = Resources.LoadAll<AudioClip>("Audio");
        AudioClip[] loadedClips = Resources.LoadAll<AudioClip>("Audio");

        foreach (AudioClip clip in loadedClips)
        {
            audioClips.Add(clip.name, clip);
        }

        foreach (var item in audioClips)
        {
            Debug.Log($"파일- {item.Key}, Clip- {item.Value} ");
        }
    }
}
