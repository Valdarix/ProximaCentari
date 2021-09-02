using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLauncher : MonoBehaviour, IDamagable
{
   public bool IsAlive { get; set; }
   public int Health { get; set; }
   [SerializeField] private GameObject _misslePrefab;
   [SerializeField] private GameObject _missileLaunchPos;
   [SerializeField] private AudioClip _hitSFX;
   [SerializeField] private GameObject explosionEffect;

   private void Start()
   {
      Health = 1500;
      IsAlive = true;
   }

   public void Spawn()
   {
      var enemyTransform = _missileLaunchPos.transform;
      var pos = enemyTransform.position;
      Instantiate(_misslePrefab, new Vector3(pos.x, pos.y, 0), _misslePrefab.transform.rotation);
   }
  
   public void Damage(int damageAmount)
   {
      Health -= damageAmount;
      AudioManager.Instance._SFXSource.PlayOneShot(_hitSFX);
      GameManager.Instance.SetLastPhase();
      if (Health <= 0)
      {
         explosionEffect.SetActive(true);
         IsAlive = false;
         Destroy(gameObject, 0.5f);
      }
   }
}
