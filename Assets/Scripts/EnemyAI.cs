
using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 15;
    [SerializeField] private Material _dissolveShader;
    [SerializeField] private AudioClip _shieldDownSFX;
    [SerializeField] private AudioClip _dissolveSFX;
    [SerializeField] private AudioClip _shieldHit;
  
    [SerializeField] private GameObject _shield;
    private float _speed = 0f;
    private Collider _collider;
    private const int CrashDamageValue = 9999;
    public int Health { get; set; }
    private bool _shieldUp;
    private Animator _anim;
    
    private Renderer _renderer;
 
    private static readonly int ShieldActive = Animator.StringToHash("ShieldActive");

    private void Start()
    {
        Health = _health;
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }

    private void OnBecameVisible()
    {
        _collider.gameObject.SetActive(true);
    }
    
    private void OnBecameInvisible()
    {
        _collider.gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        _shieldUp = (Health >= _health/2);
       
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
        
        if (Health <= 0)
        {
            _renderer.material = _dissolveShader;
            var dissolve = GetComponent<U10PS_DissolveOverTime>();
            dissolve.enabled = true;
            AudioManager.Instance._ambientSource.PlayOneShot(_dissolveSFX);
            Destroy(gameObject, 1f);
        }
      
    }

    public void ResetShieldBool()
    {
        Debug.Log("I was Called");
        _anim.SetBool(ShieldActive,false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //only call this logic if the enemy rams the player.
        if (other.CompareTag("Player"))
        {
            var hitTarget = other.GetComponent<Player_Health>().GetComponent<IDamagable>();
            hitTarget?.Damage(CrashDamageValue); 
            Destroy(gameObject);
        }
    }
}
