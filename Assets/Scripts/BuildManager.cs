using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Transform[] buildPositions;
    public GameObject[] buildPrefabs;

    private GameObject player;
    
    public int golds = 100;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Build(int index){
        if(golds < 10) return; 
        int indexpos = -1;
        for(int i = 0; i<buildPositions.Length; i++){
            if(Vector2.Distance(player.transform.position, buildPositions[i].position) < 1f){
                indexpos = i;
            }
        }
        Instantiate(buildPrefabs[index], buildPositions[indexpos].position, Quaternion.identity);
    
    }
}
