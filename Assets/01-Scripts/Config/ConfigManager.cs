using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    private void Start()
    {
        IniSliderVolume();
    }

    #region Slider Volume

    private void IniSliderVolume()
    {
        SFXSlider.value = AudioManager.s_instance.GetPPVolumeSFX();
        BGMSlider.value = AudioManager.s_instance.GetPPVolumeBGM();
    }

    public void OnSliderSFXChanged()
    {
        AudioManager.s_instance.ChangeSFXVolume(SFXSlider.value);
    }

    public void OnSliderBGMChanged()
    {
        AudioManager.s_instance.ChangeBGMVolume(BGMSlider.value);
    }

    #endregion
}
