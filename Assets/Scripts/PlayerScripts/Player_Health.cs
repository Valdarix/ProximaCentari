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
        PowerLevel = 1;
        Health = PowerLevel;
        UIManager.Instance.UpdatePlasmaLevel(Health.ToString());
        UIManager.Instance.UpdateLifeforce(Health);
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        UpdatePowerLevel(damageAmount);
        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePowerLevel(int powerChange)
    {
        PowerLevel += powerChange;
        Health = PowerLevel;
        UIManager.Instance.UpdatePlasmaLevel(Health.ToString());
        UIManager.Instance.UpdateLifeforce(Health);
    }
}
