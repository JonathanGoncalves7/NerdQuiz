using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s_instance;

    [SerializeField] List<AudioClip> BGMList;

    AudioSource audioSourceSFX;
    AudioSource audioSourceBGM;

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
        audioSourceSFX = GetComponents<AudioSource>()[0];
        audioSourceBGM = GetComponents<AudioSource>()[1];

        PlayBGM(BGMList[Random.Range(0, BGMList.Count)]);
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSourceSFX.Stop();
        audioSourceSFX.volume = PlayerPrefs.GetFloat("VOLUME_SFX", 1);
        audioSourceSFX.clip = clip;
        audioSourceSFX.Play();
    }

    public void PlayBGM(AudioClip clip)
    {
        audioSourceBGM.Stop();
        audioSourceBGM.volume = PlayerPrefs.GetFloat("VOLUME_BGM", 1);
        audioSourceBGM.clip = clip;
        audioSourceBGM.Play();
    }
}
