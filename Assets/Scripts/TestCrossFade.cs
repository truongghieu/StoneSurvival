using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrossFade : MonoBehaviour
{
    public Animator animator;
    int index = 0;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            index++;
            if(index > 2) index = 0;
            animator.CrossFade("Clip_" + index, 0.1f);
        }
    }

}
