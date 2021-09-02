using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldFlash : MonoBehaviour, IDamagable
{
    private IDamagable _damagableImplementation;
    private Animator _animator;
    [SerializeField] private AudioClip _shieldHit;
    private static readonly int ShieldFull = Animator.StringToHash("ShieldFull");
    public bool IsAlive { get; set; }
    public int Health { get; set ; }

    // Start is called before the first frame update
    void Start()
    {
        IsAlive = true;
        _animator = GetComponent<Animator>();

    }

  

    public void Damage(int damageAmount)
    {
        _animator.SetBool(ShieldFull, true);
        AudioManager.Instance._SFXSource.PlayOneShot(_shieldHit);
    }


    public void ResetShieldBool() => _animator.SetBool(ShieldFull,false);
}
