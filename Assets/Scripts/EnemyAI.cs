
using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    private float _speed = 1f;
    private Collider _collider;
    private const int CrashDamageValue = 9999;
    public int Health { get; set; }
    [SerializeField] private int _health = 10;
    [SerializeField] private Material _dissolveShader;
    private Renderer _renderer;

    [SerializeField] private AudioClip _shieldDownSFX;
    private bool _shieldUp = true;
    

    private void Start()
    {
        Health = _health;
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
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

        if (Health <= Health / 2 && _shieldUp)
        {
            AudioManager.Instance._ambientSource.PlayOneShot(_shieldDownSFX);
            _shieldUp = false;
        }
        
        if (Health <= 0)
        {
            _renderer.material = _dissolveShader;
            var dissolve = GetComponent<U10PS_DissolveOverTime>();
            dissolve.enabled = true;
            Destroy(gameObject, 1f);
        }
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
