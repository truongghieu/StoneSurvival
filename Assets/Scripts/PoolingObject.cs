using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    [Header("Object Pooling")]
    [SerializeField] private GameObject ObjectToPool;
    [SerializeField] private int amountToPool;
    [SerializeField] private bool shouldExpand = true;
    private List<GameObject> pooledObjects;

    public PoolingObject(GameObject ObjectToPool, int amountToPool, bool shouldExpand){
        this.ObjectToPool = ObjectToPool;
        this.amountToPool = amountToPool;
        this.shouldExpand = shouldExpand;
    }
    void Start()
    {
        Setup();
    }

    public void Setup(){
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++){
            GameObject obj = Instantiate(ObjectToPool);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(){
        // for(int i = 0; i < pooledObjects.Count; i++){
        //     if(!pooledObjects[i].activeInHierarchy){
        //         pooledObjects[i].SetActive(true);
        //         return;
        //     }
        // }
        // if(shouldExpand){
        //     GameObject obj = Instantiate(ObjectToPool);
        //     obj.SetActive(false);
        //     pooledObjects.Add(obj);
        //     obj.SetActive(true);
        // }
        for(int i = 0; i < pooledObjects.Count; i++){
            if (pooledObjects[i] == null) return null;
            if(!pooledObjects[i].activeInHierarchy){
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        if(shouldExpand){
            GameObject obj = Instantiate(ObjectToPool);
            obj.transform.SetParent(transform);
            pooledObjects.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void RemovePooledObject(GameObject obj){
        obj.SetActive(false);
    }
    public void RemovePooledObject(GameObject obj, float time){
        StartCoroutine(waitForDestroy(obj, time));
    }

    IEnumerator waitForDestroy(GameObject obj, float time){
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

}
