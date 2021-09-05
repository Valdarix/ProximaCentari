using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
   
    public ScriptableSpawnWave[] spawnManagerValues;
    private void Start() => StartCoroutine(SpawnEntities());

    private IEnumerator SpawnEntities()
    {
        var currentWave = 0;
        GameManager.Instance.SetWave(currentWave);
        UIManager.Instance.UpdateWaveCount(currentWave);
       
        var isPaused = GameManager.Instance.EnemiesActiveInCurrentWave > 0;
 
        for (var wave = currentWave; wave < spawnManagerValues.Length; wave++)
        {
            while (!isPaused)
            {
                UIManager.Instance.NextWave();
                UIManager.Instance.UpdateWaveCount(currentWave);
                yield return new WaitForSeconds((int)(GameManager.Instance.GetCurrentDifficulty()));
                switch (currentWave)
                {
                    case 7:
                        AudioManager.Instance.PlayClip(3);
                        break;
                    case 12:
                        AudioManager.Instance.PlayClip(2);
                        break;
                }

                var enemiesInCurrentWave = spawnManagerValues[currentWave].entityToSpawn.Length;
                GameManager.Instance.EnemiesActiveInCurrentWave = enemiesInCurrentWave;
               
                for (var i = 0; i < enemiesInCurrentWave; i++)
                {
                    yield return new WaitForSeconds(spawnManagerValues[currentWave].spawnRate + (int)GameManager.Instance.DifficultyModifier);

                    var currentEntity = Instantiate(spawnManagerValues[currentWave].entityToSpawn[i], spawnManagerValues[currentWave].spawnPoint, spawnManagerValues[currentWave].entityToSpawn[i].transform.rotation);
              
                    currentEntity.name = spawnManagerValues[currentWave].prefabName + i;
                    currentEntity.transform.parent = transform.parent;
                }
           
                isPaused =  GameManager.Instance.EnemiesActiveInCurrentWave > 0;
                currentWave++;
                GameManager.Instance.SetWave(currentWave);
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
