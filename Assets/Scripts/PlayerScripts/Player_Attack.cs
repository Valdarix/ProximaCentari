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
        PowerLevel = 1;
        _weaponFireRate = weapons[PowerLevel]._fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextTimeWeaponCanFire)
        {
            _nextTimeWeaponCanFire = Time.time + _weaponFireRate;
            Instantiate(weapons[PowerLevel], projectileStartPos.transform.position, transform.localRotation );
        }
    }
    
    public void UpdatePowerLevel(int powerChange)
    {
        PowerLevel += powerChange;
        _weaponFireRate = weapons[PowerLevel]._fireRate;
    }
}
