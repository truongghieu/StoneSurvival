using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisActive : MonoBehaviour
{

    public void AutoDisActiveObject(float time){
        StartCoroutine(DisActive(time));
    }

    IEnumerator DisActive(float time){
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
