using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour, IUpgradeable
{
    [SerializeField] private GameObject projectileStartPos;

    [SerializeField] private Weapon[] weapons;
    public int PowerLevel { get; set; }

    private float _weaponFireRate;

    private float _nextTimeWeaponCanFire;
  
    // Start is called before the first frame update
    void Start()
    {
        PowerLevel = 0;
        _weaponFireRate = weapons[PowerLevel]._fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(_weaponFireRate + "     " + _nextTimeWeaponCanFire);
        if (Input.GetButton("Fire1") && Time.time > _nextTimeWeaponCanFire)
        {
            _nextTimeWeaponCanFire = Time.time + _weaponFireRate;
            Instantiate(weapons[PowerLevel], projectileStartPos.transform.position, weapons[PowerLevel].transform.rotation);
        }
    }
    
    public void UpdatePowerLevel(int powerChange)
    {
        PowerLevel += powerChange;
        _weaponFireRate = weapons[PowerLevel]._fireRate;
    }
}
