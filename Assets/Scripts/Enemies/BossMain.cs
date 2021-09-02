using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour, IDamagable
{
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject bottom;
    public bool lastPhase;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _misslePrefab;
    [SerializeField] private GameObject _missileLaunchPos;
    [SerializeField] private GameObject _damaged;
    [SerializeField] private AudioClip _dissolveSFX;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _dissolveShader;
    
    public bool IsAlive { get; set; }
    public int Health { get; set; }
    void Start()
    {
        lastPhase = false;
        Health = 1000;
        IsAlive = true;
       
    }
    
    public void Update()
    {
        if (lastPhase)
        {
           StartCoroutine(SpawnPieces());
        }
    }

    private IEnumerator SpawnPieces()
    {
        while (lastPhase)
        {
            yield return new WaitForSeconds(.5f);
            var enemyTransform = _missileLaunchPos.transform;
            var pos = enemyTransform.position;
            Instantiate(_misslePrefab, new Vector3(pos.x, pos.y, 0), _misslePrefab.transform.rotation);
        }
     
    }

    public void Damage(int damageAmount)
    {
        if (GameManager.Instance.GetLastPhase() == true)
        {
            Health -= damageAmount;
            if (Health < 1000)
            {
                _damaged.SetActive(true);
                lastPhase = true;
                _animator.SetBool("LastPhase", true);
                var boss = FindObjectOfType<Boss>();
                boss.SetLastPhase(lastPhase);
            }

            if (Health <= 0)
            {
            
                lastPhase = false;
                _renderer.material = _dissolveShader;
                var dissolve = GetComponent<U10PS_DissolveOverTime>();
                dissolve.enabled = true;
                AudioManager.Instance._SFXSource.PlayOneShot(_dissolveSFX);
                StartCoroutine(DestroyEnemy());
            }
        }
        
    }
    
    protected IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        UIManager.Instance.EndGameText(0);
    }
}
