using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int _damageAmount;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(_damageAmount);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
