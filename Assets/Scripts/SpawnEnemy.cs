using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private PoolingObject enemyPrefap;


    [SerializeField] private float time = 2;
    [SerializeField] private int amountToSpawn = 1;
    float timef;
    GameObject obj;

    private void Start() {
        enemyPrefap = GameObject.FindGameObjectWithTag("Pool").transform.GetChild(9).GetComponent<PoolingObject>();
    }

    void Update(){
        timef += Time.deltaTime;
        if(timef >= time){
            SpawnEnemys();
            timef = 0;
        }


    }

    async void SpawnEnemys(){
        for(int i = 0; i < amountToSpawn; i++){
            obj = enemyPrefap.GetPooledObject();
            if (obj == null) await Task.Delay(1000);
            if(obj != null){
                obj.transform.position = Random.insideUnitCircle + (Vector2)transform.position;
                await Task.Delay(1000);
            }
        }
    }


}
