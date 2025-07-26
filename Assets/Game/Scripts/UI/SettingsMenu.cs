using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static Action<float> OnSensitivityChanged;

    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private Slider _soundSlider;
    private float disabledVolume = -80f;
    [SerializeField] private AudioMixer soundMixer;

    private void Awake()
    {
        _sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1.5f);
        _soundSlider.value = PlayerPrefs.GetInt("Sound", 50) / 100f;
    }

    public void HandleSensitivityChanged(Single newSensitivity)
    {
        OnSensitivityChanged?.Invoke(newSensitivity);
        PlayerPrefs.SetFloat("Sensitivity", newSensitivity);
    }

    public void HandleSoundChanged(Single sliderVolume)
    {
        float newVolume;
        if (sliderVolume == 0) newVolume = disabledVolume;
        else newVolume = Mathf.Lerp(-10f, 30f, sliderVolume);
        soundMixer.SetFloat("Master", newVolume);
        PlayerPrefs.SetInt("Sound", Convert.ToInt32(sliderVolume*100));
    }
}
