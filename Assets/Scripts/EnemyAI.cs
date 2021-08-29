
using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 15;
    [SerializeField] private int _pointValue;
    [SerializeField] private Material _dissolveShader;
    [SerializeField] private AudioClip _shieldDownSFX;
    [SerializeField] private AudioClip _dissolveSFX;
    [SerializeField] private AudioClip _shieldHit;
    
    [SerializeField] private GameObject _shield;
    private float _speed = 0f;
    private Collider _collider;
    [SerializeField] private int _crashDamageValue = 1;

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

    private void OnBecameInvisible()
    {
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
            AudioManager.Instance._ambientSource.PlayOneShot(_shieldDownSFX);
            _shield.SetActive(false);
            _shieldUp = false;
        }
        else
        {
            AudioManager.Instance._ambientSource.PlayOneShot(_shieldHit);
            _anim.SetBool(ShieldActive,true);
        }
        
        if (Health <= 0 && _currentEnemyState == EnemyState.Living)
        { 
            _renderer.material = _dissolveShader;
            var dissolve = GetComponent<U10PS_DissolveOverTime>();
            dissolve.enabled = true;
            AudioManager.Instance._ambientSource.PlayOneShot(_dissolveSFX);
            GameManager.Instance.UpdateScore(_pointValue);
            GameManager.Instance.EnemiesActiveInCurrentWave--;
            _currentEnemyState = EnemyState.Dead;
          
            Destroy(gameObject, 1f);
        }
    }
    
    public void ResetShieldBool() //called by the animation event
    {
        _anim.SetBool(ShieldActive,false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //only call this logic if the enemy rams the player.
        if (other.CompareTag("Player"))
        {
            var hitTarget = other.GetComponent<Player_Health>().GetComponent<IDamagable>();
            hitTarget?.Damage(_crashDamageValue); 
            GameManager.Instance.EnemiesActiveInCurrentWave--;
            Destroy(gameObject);
        }
    }
}
