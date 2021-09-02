using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boss : MonoBehaviour
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
    [SerializeField] private GameObject _thruster;
    
    [SerializeField] private GameObject[] _parts;
    private float _speed = 0f;
    private Collider _collider;
    private bool _lastPase;
    public bool IsAlive { get; set; }
    

    public int Health { get; set; }
    private bool _shieldUp;
    private Animator _anim;


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
    
        _anim = GetComponent<Animator>();
        _currentEnemyState = EnemyState.Living;
        IsAlive = true;
    }


 
    private void Update() => transform.Translate(Vector3.forward * (_speed * Time.deltaTime)); // not really needed? 

    private void OnBecameVisible() => _collider.gameObject.SetActive(true);

    protected IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
       _collider.gameObject.SetActive(false);
       GameManager.Instance.EnemiesActiveInCurrentWave--;
       
       Destroy(gameObject);
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
           
            IsAlive = false;
          

            foreach (var part in _parts)
            {
                part.gameObject.GetComponent<Renderer>().material = _dissolveShader;
                var dissolve = part.gameObject.GetComponent<U10PS_DissolveOverTime>();
                dissolve.enabled = true;
            }

            AudioManager.Instance._SFXSource.PlayOneShot(_dissolveSFX);
            GameManager.Instance.UpdateScore(_pointValue);
            _currentEnemyState = EnemyState.Dead;
            if (_thruster != null)
                _thruster.SetActive(false);
            StartCoroutine(DestroyEnemy());
        }
    }
    
    public void ResetShieldBool() //called by the animation event
    {
        _anim.SetBool(ShieldActive,false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _currentEnemyState != EnemyState.Dead)
        {
            var hitTarget = other.GetComponent<IDamagable>();
            if (hitTarget?.IsAlive != true) return;
            hitTarget?.Damage(_crashDamageValue); 
            GameManager.Instance.EnemiesActiveInCurrentWave--;
            Destroy(gameObject,0.1f);
        }
    }

    public void SetLastPhase(bool lastPhase)
    {
        _lastPase = lastPhase;
        _anim.SetBool("LastPhase", _lastPase);
    }
}
