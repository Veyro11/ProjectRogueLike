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

        int i = 0;

        foreach (var item in audioClips)
        {
            i++;
            Debug.Log($"파일명: {item.Key}, AudioClip: {item.Value} ,{i}");
        }
    }
}
