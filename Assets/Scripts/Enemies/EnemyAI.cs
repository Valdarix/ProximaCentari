
using System;
using System.Collections;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 15;
    [SerializeField] private int _pointValue;
    [SerializeField] private Material _dissolveShader;
    [SerializeField] private AudioClip _shieldDownSFX;
    [SerializeField] private AudioClip _dissolveSFX;
    [SerializeField] private AudioClip _shieldHit;
    [SerializeField] private int _crashDamageValue = 1; 
    [SerializeField] private GameObject _shield;
    [SerializeField] private bool _dropPowerUp;
    [SerializeField] private GameObject _powerUpObj;
    private float _speed = 0f;
    private Collider _collider;


    public int Health { get; set; }
    private bool _shieldUp;
    private Animator _anim;
    
    private Renderer _renderer;
 
    private static readonly int ShieldActive = Animator.StringToHash("ShieldActive");

    private enum EnemyState
    {
        Living,
        Dead
    }

    private EnemyState _currentEnemyState;
    private void Start()
    {
        Health = _health;
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        _anim = GetComponent<Animator>();
        _currentEnemyState = EnemyState.Living;
    }

    private void Update() => transform.Translate(Vector3.forward * (_speed * Time.deltaTime)); // not really needed? 

    private void OnBecameVisible() => _collider.gameObject.SetActive(true);

    protected void DestroyEnemy()
    {
       _collider.gameObject.SetActive(false);
       GameManager.Instance.EnemiesActiveInCurrentWave--;
       if (_dropPowerUp)
       {
           Instantiate(_powerUpObj, new Vector3(transform.localPosition.x,transform.localPosition.y, 0), quaternion.identity);
       }
       Destroy(gameObject, 1f);
    }

   public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        _shieldUp = (Health > _health/2);
       
        if (!_shieldUp && _shield.activeInHierarchy)
        {
            AudioManager.Instance._SFXSource.PlayOneShot(_shieldDownSFX);
            _shield.SetActive(false);
            _shieldUp = false;
        }
        else
        {
            AudioManager.Instance._SFXSource.PlayOneShot(_shieldHit);
            _anim.SetBool(ShieldActive,true);
        }
        
        if (Health <= 0 && _currentEnemyState == EnemyState.Living)
        { 
            _renderer.material = _dissolveShader;
            var dissolve = GetComponent<U10PS_DissolveOverTime>();
            dissolve.enabled = true;
            AudioManager.Instance._SFXSource.PlayOneShot(_dissolveSFX);
            GameManager.Instance.UpdateScore(_pointValue);
            _currentEnemyState = EnemyState.Dead;
        }
    }
    
    public void ResetShieldBool() //called by the animation event
    {
        _anim.SetBool(ShieldActive,false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var hitTarget = other.GetComponent<IDamagable>();
            hitTarget?.Damage(_crashDamageValue); 
            GameManager.Instance.EnemiesActiveInCurrentWave--;
            Destroy(gameObject,0.1f);
        }
    }
}
