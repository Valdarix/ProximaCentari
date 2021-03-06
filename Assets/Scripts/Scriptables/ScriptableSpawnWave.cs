using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ScriptableSpawnWave : ScriptableObject
{
    public string prefabName;
    public float spawnRate;
    public Vector3 spawnPoint;
    public GameObject[] entityToSpawn;
}