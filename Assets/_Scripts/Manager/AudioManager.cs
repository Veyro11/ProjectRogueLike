using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    [Header("BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    private AudioSource bgmPlayer;
    public Dictionary<string, AudioClip> bgmAudioClips;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channel;
    private AudioSource[] sfxPlayers;
    private Dictionary<string, AudioClip> sfxAudioClips;

    protected override void Awake()
    {
        base.Awake();

        InIt();
    }

    private void Start()
    {
        PlayBGM("Title");
        PlayBGM("Battle");
        SetBGMVolume(0.5f);
    }

    private void InIt()
    {
        //BGM 초기화
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        //BGMAudioClip 초기화
        bgmAudioClips = new Dictionary<string, AudioClip>();
        bgmClips = Resources.LoadAll<AudioClip>("BGM");
        foreach (AudioClip clip in bgmClips)
        {
            bgmAudioClips.Add(clip.name, clip);
        }

        int p = 0;

        foreach (var item in bgmAudioClips)
        {
            p++;
            Debug.Log($"파일명: {item.Key}, AudioClip: {item.Value} ,{p}");
        }

        //SFX 초기화
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channel];

        //SFXAudioSource 초기화
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        //SFXAudioClip 초기화
        sfxAudioClips = new Dictionary<string, AudioClip>();

        sfxClips = Resources.LoadAll<AudioClip>("Audio");
        AudioClip[] loadedClips = Resources.LoadAll<AudioClip>("Audio");

        foreach (AudioClip clip in loadedClips)
        {
            sfxAudioClips.Add(clip.name, clip);
        }

        int j = 0;

        foreach (var item in sfxAudioClips)
        {
            j++;
            Debug.Log($"파일명: {item.Key}, AudioClip: {item.Value} ,{j}");
        }

    }

    //AudioManager.Instance.PlaySFX("Jump") 이런식으로 사용 하면 됩니다.
    public void PlaySFX(string sfxName)
    {
        if (sfxAudioClips.TryGetValue(sfxName, out AudioClip clip))
        {
            // 빈 채널 찾기
            foreach (AudioSource player in sfxPlayers)
            {
                if (!player.isPlaying)
                {
                    player.clip = clip;
                    player.Play();
                    return;
                }
            }

            // 모든 채널이 사용 중이면 0번 채널 덮어쓰기
            sfxPlayers[0].clip = clip;
            sfxPlayers[0].Play();
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found!");
        }
    }

    //AudioManager.Instance.PlayBGM("Title") 이런식으로 사용 하면 됩니다.
    public void PlayBGM(string bgmName)
    {
        if (bgmAudioClips.TryGetValue(bgmName, out AudioClip clip))
        {
            if (bgmPlayer.clip == clip && bgmPlayer.isPlaying)
                return; // 이미 같은 BGM이 재생 중이면 무시

            bgmPlayer.clip = clip;
            bgmPlayer.volume = bgmVolume;
            bgmPlayer.loop = true;
            bgmPlayer.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{bgmName}' not found!");
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    // BGM 볼륨 조절
    public void SetBGMVolume(float volume)
    {
        Debug.Log($"값조절 전 {bgmPlayer.volume}");
        bgmVolume = volume;       // 내부 값 갱신
        if (bgmPlayer != null)
            bgmPlayer.volume = bgmVolume;
        Debug.Log($"값조절 후 {bgmPlayer.volume}");
    }

    // SFX 볼륨 조절
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;       // 내부 값 갱신
        if (sfxPlayers != null)
        {
            foreach (var player in sfxPlayers)
            {
                player.volume = sfxVolume;
            }
        }
    }
}
