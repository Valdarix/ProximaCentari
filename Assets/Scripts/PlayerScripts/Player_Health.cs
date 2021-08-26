using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour, IDamagable, IUpgradeable
{
  
    public int PowerLevel { get; set; }
    public int Health { get; set; }

    private void Start()
    {
        PowerLevel = 0;
        Health = PowerLevel;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePowerLevel(int powerChange)
    {
        PowerLevel += powerChange;
        
        if (powerChange < 0)
        {
            Damage(powerChange);
        }
    }
}
