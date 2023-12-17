using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateC : MonoBehaviour
{
    public float speed = 2f; 
    public Vector3 rotateDirection;


    void Update(){
        Rotate();
    }

    public void Rotate(){
        transform.Rotate(rotateDirection * speed * Time.deltaTime);
    }
}
