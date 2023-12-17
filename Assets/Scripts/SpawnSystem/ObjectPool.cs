using System.Collections.Generic;
using UnityEngine;

namespace HPoolingObject
{
public class ObjectPool
{
    private Queue<IObject> objectQueue = new Queue<IObject>();
    private GameObject objectPrefab;
    public Transform objectHolder;

    public ObjectPool(IObject iobject, int initialSize,Transform holder)
    {   
        objectPrefab = iobject.gameObject;
        this.objectHolder = holder.transform;
        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    private void AddObjectToPool()
    {
        GameObject obj = Object.Instantiate(objectPrefab);
        obj.SetActive(false);
        obj.transform.parent = objectHolder;
        obj.GetComponent<IObject>().Init(ReturnObjectToPool);
        objectQueue.Enqueue(obj.GetComponent<IObject>());
    }

    public GameObject GetObjectFromPool()
    {
        if (objectQueue.Count == 0)
        {
            AddObjectToPool();
        }

        GameObject obj = objectQueue.Dequeue().gameObject;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObjectToPool(IObject obj)
    {
        obj.gameObject.SetActive(false);
        objectQueue.Enqueue(obj);
    }
}
}
