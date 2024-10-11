using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    public float bgmTargetVolume;
    private float fadeDuration = 2f; // ���̵� ��/�ƿ� ���� �ð�
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxbgmVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { POPUP, TOUCH, TAG_OK, GETCARD_V1, GETCARD_V2, TAG_FALSE, CAMERASOUND, NEXTSCENE, COUNTDWON, NEXTPAGE }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� AudioManager�� ���ο� ���� �ε��� �� �ı����� �ʵ��� �մϴ�.
            Init();
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ϳ��� AudioManager �ν��Ͻ��� �����ϵ��� �����մϴ�.
        }
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;

        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = 1;
        bgmPlayer.clip = bgmClip;

        // SFX �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = 1;
        }
    }

    public void playSfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void PlayBgm()
    {
        if (bgmPlayer.clip != null)
        {
            bgmTargetVolume = 1;
            bgmPlayer.Play();
            StartCoroutine(FadeInBgm());
            Debug.Log("BGM playing");
        }
        else
        {
            Debug.LogWarning("BGM clip is not assigned.");
        }
    }

    public void StopBgm()
    {
        if (bgmPlayer.isPlaying)
        {
            StartCoroutine(FadeOutBgm());
            Debug.Log("BGM stopping");
        }
        else
        {
            Debug.LogWarning("BGM is not currently playing.");
        }
    }

    private IEnumerator FadeInBgm()
    {
        float startVolume = 0f;
        float endVolume = bgmTargetVolume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmPlayer.volume = Mathf.Lerp(startVolume, endVolume, t / fadeDuration);
            yield return null;
        }

        bgmPlayer.volume = endVolume;
    }

    private IEnumerator FadeOutBgm()
    {
        float startVolume = bgmPlayer.volume;
        float endVolume = 0f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmPlayer.volume = Mathf.Lerp(startVolume, endVolume, t / fadeDuration);
            yield return null;
        }

        bgmPlayer.volume = endVolume;
        bgmPlayer.Stop();
    }

    public void SoundTouch()
    {
        Debug.Log("Sound_Touch");
        playSfx(AudioManager.Sfx.TOUCH);
    }

}