using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectable
{
    [SerializeField] private int _powerValue;
    public int PowerChangeValue { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
