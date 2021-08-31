using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int _damageAmount;
    public float _fireRate = 1f;
    [SerializeField] private AudioClip _SFX;


    private void Start()
    {
        AudioManager.Instance._audioSource.PlayOneShot(_SFX);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
      
        var hitTarget = other.GetComponent<IDamagable>();
        if (hitTarget?.IsAlive == true)
        {
            hitTarget?.Damage(_damageAmount);
            Destroy(gameObject);
        }
      
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
