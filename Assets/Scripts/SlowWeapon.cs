using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlowWeapon : MonoBehaviour,IWeapon
{
    [Header("Weapon Stats")]
    [SerializeField]private float attackRange = 1.5f;
    [SerializeField]private int dame = 1;
    [Range(0,1)]private float SlowPercent = 0.2f;
    [SerializeField]private float attackSpeed = 0.5f;
    public PoolingObject bulletPool;
    [SerializeField] private int bulletAmount = 1;
    [Header("Weapon Level")]
    private int level = 0;
    [SerializeField] public int maxLevel = 10;
    [SerializeField] public Sprite[] sprites;
    private int currentSprite = 0;
    public int Level{
        get{
            return level;
        }
        set{
            level = value;
            UpgradeWeapon(level);
        }
    }
    private GameObject bullet;
    public GameObject instance { get; set; }

    private void Awake() {
        instance = this.gameObject;

    }
    private void OnEnable() {
        Shoot();
    }
    private void Start() {
        Shoot();
    }

    void Shoot(){
        // Disable all bullets before 
        for(int i = 0; i < bulletPool.transform.childCount; i++){
            bulletPool.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0; i < bulletAmount; i++){
            bullet = bulletPool.GetPooledObject();
            bullet.GetComponentInChildren<SpriteRenderer>().sprite = sprites[currentSprite];
            bullet.GetComponent<FlyAround>().target = gameObject;
            bullet.GetComponent<FlyAround>().flySpeed = Random.Range(attackSpeed - 0.5f * attackSpeed, attackSpeed + 0.5f * attackSpeed );
            bullet.GetComponent<FlyAround>().flyRange = attackRange;
            bullet.GetComponent<Star>().dame = dame;
            bullet.GetComponent<Star>().slowPercent = SlowPercent;
            bullet.GetComponent<FlyAround>().Fly();
        }
    }


    public void UpgradeWeapon(int lvl){
        currentSprite = lvl;
        if(currentSprite > sprites.Length - 1) currentSprite = sprites.Length - 1;
        attackSpeed = Mathf.Max(0.1f, 0.5f - 0.05f * lvl);
        dame = 1 + 1 * lvl;
        SlowPercent = lvl > 15 ? 0.9f : 0.2f + 0.05f * lvl;
        bulletAmount = Mathf.Min((int)(1 + lvl / 3f), 5);
        attackRange = 2f +  0.5f * lvl;
        if(attackRange > 5f) attackRange = 5f;
        Shoot();
    }

    // private void OnDisable() {
    //     for(int i = 0; i < bulletPool.transform.childCount; i++){
    //         bulletPool.transform.GetChild(i).gameObject.SetActive(false);
    //     }
    // }
}
