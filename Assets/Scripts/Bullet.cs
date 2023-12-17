using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dame = 1;

    public Rigidbody2D rb;

    void Start()
    {
        
    }


    public void MoveToDirection(Vector3 dir,float speed){
        // transform.Rotate(0, 0, Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg -90);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90);

        rb.velocity = new Vector2(dir.x * speed * Time.deltaTime, dir.y * speed * Time.deltaTime);

    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().TakeDamage(dame);
            gameObject.SetActive(false);
        }
    }
}
