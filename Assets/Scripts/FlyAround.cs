using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyAround : MonoBehaviour
{
    public float flySpeed = 1f;
    public float flyRange = 1f;
    public GameObject target;

    virtual public void Fly(){
        Vector2 randomTarget = (Vector2)target.transform.position + Random.insideUnitCircle * flyRange;
        transform.DOMove(randomTarget, flySpeed).OnComplete(Fly);
    }
}
