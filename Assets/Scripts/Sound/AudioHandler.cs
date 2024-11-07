using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    private void Start()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    public void SetMasterVolume(float volume) => AudioManager.Instance.SetMasterVolume(volume);

    public void SetMusicVolume(float volume) => AudioManager.Instance.SetMusicVolume(volume);

    public void SetSFXVolume(float volume) => AudioManager.Instance.SetSFXVolume(volume);
}
