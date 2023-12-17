using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Mana : MonoBehaviour
{   

    [Header("Selft Config")]
    [SerializeField] private float timeEffect;
    [SerializeField] private int exp = 1;
    [SerializeField] private int gold = 1;

    // Active function
    public void SpawnMana(Vector2 pos,float radius = 1){
        // get random point from pos with radius 1
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        transform.DOMove(pos + randomPoint, timeEffect);
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            // check selft tag 
            if(this.tag == "Mana"){
                // add mana
                LevelManager.Instance.AddExp(exp);
            }
            // destroy mana
            this.GetComponent<AutoDisActive>().AutoDisActiveObject(0);
        }

    }

}
