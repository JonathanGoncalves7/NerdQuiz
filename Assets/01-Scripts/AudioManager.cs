using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    const string VOLUME_BGM = "VOLUME_BGM";
    const string VOLUME_SFX = "VOLUME_SFX";

    public static AudioManager s_instance;

    [SerializeField] AudioSource AudioSourceBGM;
    [SerializeField] AudioSource AudioSourceSFX;

    [SerializeField] List<AudioClip> BGMList;
    [SerializeField] AudioClip ButtonClick;

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IniAudioVolume();
        PlayBGM(BGMList[UnityEngine.Random.Range(0, BGMList.Count)]);
    }

    #region Volume

    private void IniAudioVolume()
    {
        AudioSourceBGM.volume = GetPPVolumeBGM();
        AudioSourceSFX.volume = GetPPVolumeSFX();
    }

    public float GetPPVolumeBGM()
    {
        return PlayerPrefs.GetFloat(VOLUME_BGM, 0.5f);
    }

    public float GetPPVolumeSFX()
    {
        return PlayerPrefs.GetFloat(VOLUME_SFX, 0.5f);
    }

    private void ChangeVolume(AudioSource audioSource, string playerPrefsName, float volume)
    {
        volume = Mathf.Max(0, volume);
        volume = Mathf.Min(1, volume);

        PlayerPrefs.SetFloat(playerPrefsName, volume);
        audioSource.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        ChangeVolume(AudioSourceSFX, VOLUME_SFX, volume);
    }

    public void ChangeBGMVolume(float volume)
    {
        ChangeVolume(AudioSourceBGM, VOLUME_BGM, volume);
    }

    #endregion

    #region Play Clip

    private void PlayClip(AudioSource audioSource, AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        PlayClip(AudioSourceSFX, clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        PlayClip(AudioSourceBGM, clip);
    }

    public void PlayButtonClick()
    {
        PlaySFX(ButtonClick);
    }


    public AudioClip GetButtonClick()
    {
        return ButtonClick;
    }

    #endregion

}
