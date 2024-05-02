using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource masterSource, musicSource, sfxSource;

    private void Awake()
    {
        if (Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }

        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Music not found");
        }
        else
        {
            musicSource.clip = s.AudioClip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("SFX not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.AudioClip);
        }
    }
    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = _SFXSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
