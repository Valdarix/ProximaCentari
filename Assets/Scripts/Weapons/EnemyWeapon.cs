
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int _damageAmount = 1;
    public float _fireRate = 1f;
    [SerializeField] private AudioClip _SFX;


    private void Start()
    {
        AudioManager.Instance._audioSource.PlayOneShot(_SFX);
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
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
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

