using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWeapon : MonoBehaviour,IWeapon
{
    [Header("Weapon Properties")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int dame = 10;
    [SerializeField] private float bulletAmount = 1;
    [Header("Weapon Settings")]
    [SerializeField] private PoolingObject RocketPooling;
    [Header("Weapon Upgrade")]
    [SerializeField] private int level = 0;
    [SerializeField] public int maxLevel = 2;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    [SerializeField] private Sprite[] weaponSprite;
    private int currentSprite = 0;
    [SerializeField] private GameObject[] enemysTarget;
    private float timeDuration = 2f;
    public GameObject instance { get; set; }
    public int Level{
        get{
            return level;
            }
        set{
            level = value;
            UpgradeWeapon(level);
        }
    }
    private Transform player;
    
    private void Awake() {
        instance = this.gameObject;
    }
    void Start(){
        Setup();
        StartCoroutine(Fire());
    }

    void Update(){
        
    }

    void Setup(){
        enemysTarget = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    public void RocketFired(){
        for(int i = 0; i < bulletAmount; i++){
            GameObject rocket = RocketPooling.GetPooledObject();
            if(rocket == null) return;
            rocket.GetComponent<Rocket>().dame = dame;
            rocket.transform.position = player.position;    
            rocket.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponSprite[currentSprite];
            rocket.GetComponent<Rocket>().target = Random.insideUnitCircle  + (Vector2) player.position;
            rocket.SetActive(true);
        }
    }

    public void UpgradeWeapon(int lvl){
        currentSprite = lvl;
        if (currentSprite > weaponSprite.Length - 1){
            currentSprite = weaponSprite.Length - 1;
        }
        bulletAmount = 1 + lvl;
        dame = 10 + lvl * 10;
        timeDuration = Mathf.Max(2f - lvl * 0.1f,0.2f);
    }

    private IEnumerator Fire(){
        while(true){
            yield return new WaitForSeconds(timeDuration);
            RocketFired();
        }
    }
}
