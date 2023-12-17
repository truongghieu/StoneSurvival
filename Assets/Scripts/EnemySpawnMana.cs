using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;

public class EnemySpawnMana : MonoBehaviour
{
    [SerializeField] private PoolingObject ManaObject;
    // make slider for amount of mana
    [Range(2, 1000)] [SerializeField] private int amountOfMana;
    [Range(1,10)] [SerializeField] private float randomAmountRange = 2f;
    [Range(1,100)] [SerializeField] private float timeManaExist = 2f;
    [Range(1,10)] [SerializeField] private float radius = 1f;
    GameObject mana;

    

    private void Start() {
        // ManaObject = GameObject.Find("PoolingManager").transform.GetChild(7).GetComponent<PoolingObject>();
        // find game object with tag Pool but it on the scene and this object is the child of PoolingManager
        // ManaObject = transform.parent.parent.GetChild(7).GetComponent<PoolingObject>();
        ManaObject = GameObject.FindGameObjectWithTag("Pool").transform.GetChild(7).GetComponent<PoolingObject>();
    }

    public void SpawnMana(){
       for(int i = 0; i < Random.Range(amountOfMana-randomAmountRange, amountOfMana); i++){
                mana = ManaObject.GetPooledObject();
                if(mana == null) return;
                mana.transform.position = transform.position;
                mana.GetComponent<Mana>().SpawnMana(transform.position,radius);
                mana.GetComponent<AutoDisActive>().AutoDisActiveObject(Random.Range(0, timeManaExist));
            }
    }
    
}
