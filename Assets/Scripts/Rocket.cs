using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour
{

    public Rigidbody2D rb;

    public Vector2 target;
    public float defautSpeed = 5f;
    public float RotateSpeed = 200f;
    public float defaultAttackRange = 1f;
    public int dame = 10;
    public float timeToComback = 1f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        // try to find enemy
        
    }

    void Update(){
        FlyToTarget(target, defautSpeed);
    }

    public void FlyToTarget(Vector2 target, float speed){
        Vector2 direction = target - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * RotateSpeed;
        rb.velocity = transform.up * speed;

    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().TakeDamage(dame);
            this.gameObject.SetActive(false);
        }
    }
    
}
