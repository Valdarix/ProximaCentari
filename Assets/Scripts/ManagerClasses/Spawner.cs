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
        foreach (var wave in spawnManagerValues)
        {
            yield return new WaitForSeconds(1.5f);
            var currentSpawnPointIndex = 0;
        
            for (var i = 0; i < wave.numberOfPrefabsToCreate; i++)
            {
                
                var randomEntity = Random.Range(0, entityToSpawn.Length);
                // Creates an instance of the prefab at the current spawn point.
                var currentEntity = Instantiate(entityToSpawn[randomEntity], wave.spawnPoints[currentSpawnPointIndex], entityToSpawn[randomEntity].transform.rotation);

                // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
                currentEntity.name = wave.prefabName + _instanceNumber;

                // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
                currentSpawnPointIndex = (currentSpawnPointIndex + 1) % wave.spawnPoints.Length;

                _instanceNumber++;
            }
        }
    }
}
