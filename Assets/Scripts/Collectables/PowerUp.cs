using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectable
{
    [SerializeField] private int _powerValue;
    [SerializeField] private AudioClip _sfxAudioClip;
    public int PowerChangeValue { get; set; }
    private const int Score = 200;


    private void Start()
    {
        Destroy(gameObject,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance._SFXSource.PlayOneShot(_sfxAudioClip);
        GameManager.Instance.UpdateScore(Score);
        var hitTarget = other.GetComponent<IUpgradeable>();
        hitTarget?.UpdatePlasmaLevel(_powerValue);
        Destroy(gameObject);
    }
    
    
}
