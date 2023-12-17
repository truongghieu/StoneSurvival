using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int dame = 1;
    public float slowPercent = 0.5f;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().TakeDamage(dame);
            other.gameObject.GetComponent<Enemy>().Slow(slowPercent);
        }
    }
}
