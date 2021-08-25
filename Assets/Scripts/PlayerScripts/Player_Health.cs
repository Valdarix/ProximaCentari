using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed = 1f;
   
    [SerializeField] private int _health;
    public int Health { get; set; }

    private void Start()
    {
        Health = _health;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
