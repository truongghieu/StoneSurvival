using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BuildingInteraction : MonoBehaviour
{
    public RectTransform buildTransform;
    public Vector2 buildPosition_0,buildPosition_1;

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("enter");
        if(other.gameObject.tag == "Player"){
            buildTransform.DOAnchorPos(buildPosition_0, 0.5f);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        Debug.Log("exit");
        if(other.gameObject.tag == "Player"){
            buildTransform.DOAnchorPos(buildPosition_1, 0.5f);
        }
    }
}
