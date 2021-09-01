using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLauncher : MonoBehaviour
{
   [SerializeField] private GameObject _misslePrefab;
   [SerializeField] private GameObject _missileLaunchPos;
   public void Spawn()
   {
      var enemyTransform = _missileLaunchPos.transform;
      var pos = enemyTransform.position;
      Instantiate(_misslePrefab, new Vector3(pos.x, pos.y, 0), _misslePrefab.transform.rotation);
   }
}
