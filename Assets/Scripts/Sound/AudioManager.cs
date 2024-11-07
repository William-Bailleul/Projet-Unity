using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    private const string MASTER_VOLUME_PREFS = "MasterVolume";
    private const string MUSIC_VOLUME_PREFS = "MusicVolume";
    private const string SFX_VOLUME_PREFS = "SFXVolume";
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource masterSource, musicSource, sfxSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField, Range(0.0f, 1.0f)] private float _defaultMasterVolume;
    [SerializeField, Range(0.0f, 1.0f)] private float _defaultMusicVolume;
    [SerializeField, Range(0.0f, 1.0f)] private float _defaultSFXVolume;

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
            return;
        }

        LoadVolume();
    }
    private void Start()
    {
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
    public void SetMasterVolume(float volume)
    {
        float volE = GetFixedVolume(volume);
        _audioMixer.SetFloat("Master",volE);
        PlayerPrefs.SetFloat(MASTER_VOLUME_PREFS, volume);
    }
    public void SetMusicVolume(float volume)
    {

        float volE = GetFixedVolume(volume);
        _audioMixer.SetFloat("Music", volE);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PREFS, volume);
    }
    public void SetSFXVolume(float volume)
    {
        float volE = GetFixedVolume(volume);
        _audioMixer.SetFloat("SFX", volE);
        PlayerPrefs.SetFloat(SFX_VOLUME_PREFS, volume);
    }

    private static float GetFixedVolume(float volume) => Mathf.Log10(volume) * 20;

    private void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_PREFS, _defaultMasterVolume);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREFS, _defaultMusicVolume);
        float SFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PREFS, _defaultSFXVolume);
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(SFXVolume);
    }
}
