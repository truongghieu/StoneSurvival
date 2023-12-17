using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector2 Maximum;
    public Vector2 Minimum;

    void Update(){
        Following();
    }

    void Following(){
        
        float x = Mathf.Clamp(target.position.x,Minimum.x,Maximum.x);
        float y = Mathf.Clamp(target.position.y,Minimum.y,Maximum.y);

        Vector3 desiredPosition = new Vector3(x,y,transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position = smoothedPosition;
    }
}
