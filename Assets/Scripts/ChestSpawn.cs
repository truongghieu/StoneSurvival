using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    [SerializeField] private PoolingObject chestPool;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private float spawnChance = 0.5f;
    private GameObject chest;
    private bool isSpawn = false;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while(true){
            if(isSpawn) yield return null;
            if(Random.Range(0f, 1f) > spawnChance) yield return null;
            isSpawn = true;
            chest = chestPool.GetPooledObject();
            if(chest == null) yield return null;
            chest.transform.position = new Vector2(Random.Range(-spawnRange, spawnRange), spawnHeight);
            chest.SetActive(true);
            StartCoroutine(wait());
        }
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(spawnDelay);
        isSpawn = false;
    }
}
