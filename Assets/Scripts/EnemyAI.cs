
using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed = 1f;
    private Collider _collider;
    private const int CrashDamageValue = 9999;
    public int Health { get; set; }
    [SerializeField] private int _health = 10;

    private void Start()
    {
        Health = _health;
        _collider = GetComponent<Collider>();
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
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //only call this logic if the enemy rams the player.
        if (other.CompareTag("Player"))
        {
            var hitTarget = other.GetComponent<Player_Health>().GetComponent<IDamagable>();
            hitTarget?.Damage(CrashDamageValue); 
            Destroy(gameObject);
        }
    }
}