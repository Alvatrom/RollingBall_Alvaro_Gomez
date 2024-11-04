using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music Sounds")]
    [SerializeField] private Sound[] musicSounds;
    [Header("SFX Sounds")]
    [SerializeField] private Sound[] sfxSounds;


    [SerializeField] private AudioSource musicSource, audioSourceSfx;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReproducirSonido(AudioClip clip)
    {
        //Reproducir una vez el sonido
        audioSourceSfx.PlayOneShot(clip);
    }

    private void Start()
    {
        PlayMusic("PlayMode");
    }

    public void PlayMusic(string name)//busca por nombre en el array creado los siguientes sonidos
    {
        Sound s =Array.Find(musicSounds, x => x.name == name);
        if(s == null)
        {
            Debug.Log("Musica no encontrada");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)//busca por nombre en el array creado los siguientes sonidos
    {
        //preguntar esto
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Musica no encontrada");
        }
        else
        {
            audioSourceSfx.PlayOneShot(s.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        audioSourceSfx.mute = !audioSourceSfx.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        audioSourceSfx.volume = volume;
    }

}
