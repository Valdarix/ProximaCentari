using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour, IDamagable, IUpgradeable
{
    [SerializeField] private float _speed;
    private Animator _anim;
    private PlayerState _currentPlayerState;
    public int Health { get; set; }
    [SerializeField] private GameObject _thruster;
    [SerializeField] private GameObject _breakParticles;
    [SerializeField] private AudioClip _sfxDeath;
    
    [SerializeField] private GameObject projectileStartPos;

    [SerializeField] private Weapon[] weapons;
    public bool IsAlive { get; set; }
    private int PowerLevel { get; set; }

    private float _weaponFireRate;

    private float _nextTimeWeaponCanFire;


    private enum PlayerState
    {
        Idle,
        Horizontal,
        Vertical,
        PowerChange,
        Death
    }
  
    // Start is called before the first frame update
    private void Start()
    {
        _currentPlayerState = PlayerState.Idle;
        
        _anim = GetComponent<Animator>();
        
        Health = 0;
        UIManager.Instance.UpdateLifeforce(Health);
       
        UpdatePlasmaLevel(0);
       
        _weaponFireRate = weapons[PowerLevel]._fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleStateChange();
        IsAlive = _currentPlayerState != PlayerState.Death;
    }
    
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextTimeWeaponCanFire && PowerLevel >= 0)
        {
            _nextTimeWeaponCanFire = Time.time + _weaponFireRate;
            Instantiate(weapons[PowerLevel], projectileStartPos.transform.position, transform.localRotation );
        }
    }

    private void UpdatePlayerState(PlayerState newState) => _currentPlayerState = newState;

    private void HandleStateChange()
    {
        switch (_currentPlayerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Horizontal:
                break;
            case PlayerState.Vertical:
                break;
            case PlayerState.PowerChange:
                break;
            case PlayerState.Death:
                _anim.SetTrigger("Die");
                break;
            default:
                Debug.LogError("Player has no state");
                break;
        }
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");

        var vertical = Input.GetAxisRaw("Vertical");
        _thruster.SetActive(horizontal > 0 || (vertical != 0 && horizontal !<= 0));
        _breakParticles.SetActive(horizontal <= 0 && vertical == 0);
       _anim.SetFloat("VerticalInput", vertical);
        var moveVector = new Vector3(horizontal, vertical, 0);
        
        transform.Translate(moveVector * (_speed * Time.deltaTime), Space.World);
        
        var position = transform.position;
        var yClamp = Mathf.Clamp(position.y, -7f, 7f);
        var xClamp = Mathf.Clamp(position.x, -11.5f, 11.5f);
        
        position = new Vector3(xClamp, yClamp, 0);
        transform.position = position;
    }
    
    public void Damage(int damageAmount)
    {
        
        PowerLevel -= damageAmount;
        Health = PowerLevel;
        UIManager.Instance.UpdateLifeforce(Health);
        var plasmaGaugeValue = PowerLevel switch
        {
            0 => 0.2f,
            1 => 0.4f,
            2 => 0.6f,
            3 => 0.8f,
            4 => 1f,
            _ => 0f
        };
        UIManager.Instance.UpdatePlasmaLevel(plasmaGaugeValue);
      
        if (Health < 0)
        {
            GetComponent<Player_Move>().UpdatePlayerState(PlayerState.Death);
            GetComponent<AudioSource>().PlayOneShot(_sfxDeath);
            
        }
        
    }

    public void UpdatePlasmaLevel(int powerChange)
    {
        PowerLevel += powerChange;
        Health = PowerLevel;
        UIManager.Instance.UpdateLifeforce(Health);
        
        var plasmaGaugeValue = PowerLevel switch
        {
            0 => 0.2f,
            1 => 0.4f,
            2 => 0.6f,
            3 => 0.8f,
            4 => 1f,
            _ => 0f
        };
        UIManager.Instance.UpdatePlasmaLevel(plasmaGaugeValue);
        
        if (PowerLevel !<0 && PowerLevel !>4)
            _weaponFireRate = weapons[PowerLevel]._fireRate;
    }

    protected void DieNotifier()
    {   
        Destroy(gameObject);
        UIManager.Instance.EndGameText(1);
        
    }

}
