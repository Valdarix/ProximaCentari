using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
   
    public ScriptableSpawnWave[] spawnManagerValues;
    private void Start() => StartCoroutine(SpawnEntities());

    private IEnumerator SpawnEntities()
    {
        var currentWave = GameManager.Instance.GetWave();
        var isPaused = GameManager.Instance.EnemiesActiveInCurrentWave > 0;
        
        foreach (var wave in spawnManagerValues)
        {
            while (!isPaused)
            {
                UIManager.Instance.NextWave();
                yield return new WaitForSeconds((int)(GameManager.Instance.GetCurrentDifficulty()));
                if (currentWave == 7)
                {
                    AudioManager.Instance.PlayClip(3);
                }
                if (currentWave == 12)
                {
                    AudioManager.Instance.PlayClip(2);
                }
                
                var enemiesInCurrentWave = wave.entityToSpawn.Length;
                GameManager.Instance.EnemiesActiveInCurrentWave = enemiesInCurrentWave;
                for (var i = 0; i < enemiesInCurrentWave; i++)
                {
                    yield return new WaitForSeconds(wave.spawnRate);

                    var currentEntity = Instantiate(wave.entityToSpawn[i], wave.spawnPoint, wave.entityToSpawn[i].transform.rotation);
              
                    currentEntity.name = wave.prefabName + i;
                    currentEntity.transform.parent = transform.parent;
                }
           
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
