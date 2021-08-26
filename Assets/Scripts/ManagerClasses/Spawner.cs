using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The GameObject to instantiate.
    public GameObject[] entityToSpawn;

    // An instance of the ScriptableObject defined above.
    public ScriptableSpawnWave[] spawnManagerValues;

    // This will be appended to the name of the created entities and increment when each is created.
    int _instanceNumber = 1;

    private void Start()
    {
        StartCoroutine(SpawnEntities());
    }

    private IEnumerator SpawnEntities()
    {
        var enemyToSpawn = -1;
        var currentWave = 0;
        foreach (var wave in spawnManagerValues)
        {
            yield return new WaitForSeconds(1f);

            for (var i = 0; i < wave.numberOfPrefabsToCreate; i++)
            {
                yield return new WaitForSeconds(1f);

                if (currentWave < 4)
                {
                    enemyToSpawn = currentWave;
                }
                else
                {
                    enemyToSpawn = Random.Range(0, entityToSpawn.Length);
                }

                var currentEntity = Instantiate(entityToSpawn[enemyToSpawn], wave.spawnPoint, entityToSpawn[enemyToSpawn].transform.rotation);
              
                currentEntity.name = wave.prefabName + i;
                currentEntity.transform.parent = transform.parent;

                _instanceNumber++;
            }
            currentWave++;
        }
    }
}
