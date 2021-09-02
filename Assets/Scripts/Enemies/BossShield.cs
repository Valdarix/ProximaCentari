using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour, IDamagable
{
    public int Health { get; set; }
    private bool _shieldUp;
    private int _shieldStrength;
    [SerializeField] private GameObject _shield;
    [SerializeField] private AudioClip _shieldDownSFX;
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private GameObject explosionEffect;

    private void Start()
    {
        _shieldStrength = 1500;
        Health = _shieldStrength;
        IsAlive = true;
        _shieldUp = true;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        _shieldUp = (Health > 0); 
        AudioManager.Instance._SFXSource.PlayOneShot(_hitSFX);
        
        if (!_shieldUp && _shield.activeInHierarchy)
        {
            AudioManager.Instance._SFXSource.PlayOneShot(_shieldDownSFX);
            _shield.SetActive(false);
            _shieldUp = false;
        }

        if (Health <= 0)
        {
            explosionEffect.SetActive(true);
            IsAlive = false;
            Destroy(gameObject, 0.5f);
        }
    }

    public bool IsAlive { get; set; }
}
