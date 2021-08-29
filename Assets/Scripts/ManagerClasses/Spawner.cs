using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    // The GameObject to instantiate.
    public GameObject[] entityToSpawn;

    // An instance of the ScriptableObject defined above.
    public ScriptableSpawnWave[] spawnManagerValues; 
   private int _enemiesActiveInWave;
 
    private void Start() => StartCoroutine(SpawnEntities());

    private IEnumerator SpawnEntities()
    {
        var currentWave = 0;
        var isPaused = GameManager.Instance.EnemiesActiveInCurrentWave > 0;
        foreach (var wave in spawnManagerValues)
        {
            var enemiesInCurrentWave = 0;
            GameManager.Instance.EnemiesActiveInCurrentWave = enemiesInCurrentWave;
            
            while (!isPaused)
            {
                UIManager.Instance.NextWave();
                yield return new WaitForSeconds((int)(GameManager.Instance.GetCurrentDifficulty()));
               
                enemiesInCurrentWave = wave.numberOfPrefabsToCreate;
                for (var i = 0; i < enemiesInCurrentWave; i++)
                {
                    yield return new WaitForSeconds(1f);

                    var enemyToSpawn = currentWave < 4 ? currentWave : Random.Range(0, entityToSpawn.Length);

                    var currentEntity = Instantiate(entityToSpawn[enemyToSpawn], wave.spawnPoint, entityToSpawn[enemyToSpawn].transform.rotation);
              
                    currentEntity.name = wave.prefabName + i;
                    currentEntity.transform.parent = transform.parent;
                    _enemiesActiveInWave++;
                }
                GameManager.Instance.EnemiesActiveInCurrentWave = _enemiesActiveInWave; 
                isPaused =  GameManager.Instance.EnemiesActiveInCurrentWave > 0;
                currentWave++;
            }
            while (isPaused) 
            {
                
                isPaused = GameManager.Instance.EnemiesActiveInCurrentWave > 0;
                yield return null;
            }
            yield return new WaitForSeconds(2.5f);
          
        }
    }
}
