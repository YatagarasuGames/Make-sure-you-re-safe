using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsLoader : MonoBehaviour
{
    [SerializeField] private AudioMixer _soundMixer;
    private void Start()
    {
        float newVolume;
        if (((float)PlayerPrefs.GetInt("Sound", 50))/100 == 0) newVolume = -80;
        else newVolume = Mathf.Lerp(-10f, 30f, ((float)PlayerPrefs.GetInt("Sound", 50))/100);
        _soundMixer.SetFloat("Master", newVolume);
    }
}
