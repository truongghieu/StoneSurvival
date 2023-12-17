using System.Collections;
using UnityEngine;
using HPoolingObject;
using System.Collections.Generic;
using System;

public class SpawnSystem : MonoBehaviour
{
    [Header("Pooling Object Config")]
    [SerializeField] private List<GameObject> GameObjectsToSpawn;
    [SerializeField] private int initialSize = 10;
    public Dictionary<string, ObjectPool> objectPools = new Dictionary<string, ObjectPool>();

    [Header("Spawn Config")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] public int round = 0;
    [SerializeField] private int maxRound = 100;

    [Header("Spawn Round Config")]
    [SerializeField] public SpawnRoundConfig[] spawnRoundConfigs;


    public static Action OnSpawnStart;

    void Start()
    {
        Init(GameObjectsToSpawn, initialSize);
        StartCoroutine(SpawnRounds());
    }

    public void Init(List<GameObject> GameObjectsToSpawn, int initialSize)
    {
        this.GameObjectsToSpawn = GameObjectsToSpawn;
        foreach (GameObject obj in GameObjectsToSpawn)
        {
            try{
                if(!obj.TryGetComponent<IObject>(out IObject iobject)){
                    Debug.LogError("Object " + obj.name + " doesn't implement IObject interface.");
                    continue;
                }
            }catch(Exception e){
                Debug.LogError("Object " + obj.name + " doesn't implement IObject interface.");
                continue;
            }
            objectPools.Add(obj.GetComponent<IObject>().UniqueID, new ObjectPool(obj.GetComponent<IObject>(), initialSize,this.transform));
        }
    }

    public GameObject SpawnFromPool(string UniqueID, Vector3 position, Quaternion rotation)
    {
        if (!objectPools.ContainsKey(UniqueID))
        {
            Debug.LogWarning("Pool with UniqueID " + UniqueID + " doesn't exist.");
            return null;
        }
        GameObject objectToSpawn = objectPools[UniqueID].GetObjectFromPool();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        return objectToSpawn;
    }


    IEnumerator SpawnRounds()
    {
        for (int i = 0; i < spawnRoundConfigs.Length; i++)
        {
            SpawnRoundConfig roundConfig = spawnRoundConfigs[i];
            yield return new WaitForSeconds(roundConfig.timeBetweenRound);

            OnSpawnStart?.Invoke();
            // Sound play
            SoundManager.Play("waveSpawn");
            for (int j = 0; j < roundConfig.waveQuantity; j++)
            {
                SpawnEnemy(roundConfig.typeOfEnemy);
                yield return new WaitForSeconds(roundConfig.timeBetweenSpawn);
            }  
            for (int j = 0; j < roundConfig.amountOfBoss; j++)
            {
                SpawnEnemy(roundConfig.bossType);
                yield return new WaitForSeconds(roundConfig.timeBetweenSpawn * 2);
            }

            round++;
        }
    }

    void SpawnEnemy(int typeOfEnemy)
    {
        if (typeOfEnemy >= GameObjectsToSpawn.Count)
        {
            typeOfEnemy = GameObjectsToSpawn.Count - 1;
            Debug.LogWarning("Reach max level of enemy.");
        }
        GameObject enemyPrefab = GameObjectsToSpawn[typeOfEnemy];
        Transform spawnPoint = GetRandomSpawnPoint();

        string uniqueID = enemyPrefab.GetComponent<IObject>().UniqueID;
        GameObject spawnedEnemy = SpawnFromPool(uniqueID, spawnPoint.position, spawnPoint.rotation);
        // Customize spawned enemy (if needed)
        // Example: spawnedEnemy.GetComponent<EnemyScript>().SetCustomParameters(...);
    }




    GameObject GetHigherLevelEnemy()
    {
        int level = UnityEngine.Random.Range(0, GameObjectsToSpawn.Count);
        return GameObjectsToSpawn[Mathf.Clamp(level, 0, GameObjectsToSpawn.Count - 1)];
    }

    Transform GetRandomSpawnPoint()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        return spawnPoints[spawnPointIndex];
    }
}


[System.Serializable]
public struct SpawnRoundConfig{
    public int typeOfEnemy;
    public int waveQuantity;
    public float timeBetweenRound;
    public float timeBetweenSpawn;
    public int bossType;
    public int amountOfBoss;
}