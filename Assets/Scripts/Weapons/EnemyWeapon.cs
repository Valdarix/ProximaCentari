
using System;
using System.Collections;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int _damageAmount = 1;
    public int _fireRate = 1;
    [SerializeField] private AudioClip _SFX;
    private Vector3 _randomLocation;
    [SerializeField] private GameObject _explosion;

    private Animator _animator;


    private void Start()
    {
        AudioManager.Instance._SFXSource.PlayOneShot(_SFX);
        if (_fireRate == 2)
        {
            StartCoroutine(AutoBoom());
        }

        _animator = GetComponent<Animator>();
        GetRandomLocation();
    }

    private IEnumerator AutoBoom()
    {
        yield return new WaitForSeconds(5f);
        _animator.SetBool("GoBoom", true);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject,.1f);
    }
    
    void Update()
    {
        var step =  speed * Time.deltaTime;
        if (_fireRate == 2)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, _randomLocation, step);
        }
        else
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
        if (Vector3.Distance(transform.position, _randomLocation) < 0.001f)
        {
            // Swap the position of the cylinder.
            GetRandomLocation();
        }
      
    }

    private void GetRandomLocation()
    {
        var randomX = Random.Range(-12f,6f);
        Mathf.Clamp(randomX, -12f, 6f);
        var randomY = Random.Range(-7f,6.5f);
        Mathf.Clamp(randomX, -7f, 6.5f);
        
        _randomLocation = new Vector3(randomX, randomY, 0);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            var hitTarget = other.GetComponent<IDamagable>();
            if (hitTarget?.IsAlive != true) return;
            hitTarget?.Damage(_damageAmount); 
            
            Destroy(gameObject,0.1f);
        }
      
    }

    private void GoBoom()
    {
        _explosion.SetActive(true);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

