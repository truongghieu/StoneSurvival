using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private float timeToDisActive = 0.5f;
    [SerializeField] private PoolingObject effectObject;
    [SerializeField] private String objectTag;
    private GameObject effect;


    void Start()
    {
        effectObject = GameObject.Find("PoolingManager").transform.GetChild(4).GetComponent<PoolingObject>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == objectTag){
            effect = effectObject.GetPooledObject();
            if(effect == null) return;
            effect.transform.position = transform.position;
            effect.GetComponent<AutoDisActive>().AutoDisActiveObject(timeToDisActive);
        }
    }
}
