using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance => _instance;

    [SerializeField] private List<AudioClip> music;
    public AudioSource _audioSource;
    public AudioSource _ambientSource;
    public AudioSource _SFXSource;

    private void Awake()
    {
        _instance = this;
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
      
    }

    public void PlayClip(int clipLevelToPlay)
    {
        _audioSource.clip = music[clipLevelToPlay];
        _audioSource.Play();
    }
}
