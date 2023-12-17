using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SuperPowerGun : MonoBehaviour,IWeapon
{
    [Header("Weapon Properties")]
    public float speed = 10f;

    public GameObject instance { get; set; }
    public int dame = 1;
    public float timeToDestroy = 2f;
    // protected int level = 0;
    public int maxLevel = 23;
    private int level = 0;
    public int Level{
        get{return level;}
        set{
            level = value;
            UpgradeWeapon(level);
        }
    }
    
    public Joystick joystick;
    public Toggle toggle;
    public float attackSpeed = 0.5f;
    [Header("Weapon Setting")]
    public bool isAttack = false;
    public Transform weaponTransform;
    [SerializeField] public Transform visualTransform;
    [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public PoolingObject bulletPooling;
    public Sprite[] gunSprite;
    private int currentSprite = 0;
    public SpriteRenderer gunSpriteRenderer;
    // do not show in inspector
    [HideInInspector] public GameObject bullet;

    private void Awake() {
        instance = this.gameObject;
    }
    virtual public void Start(){
        // Level = 5;
       
    }

    void Update(){
        // Debug.Log(level);
        Hit();
        Rotate();
        Weapop_Flip();
    }

    virtual public void Rotate(){
        // if(isAttack) return;
        if(joystick.Horizontal == 0 && joystick.Vertical == 0) return;
        
        weaponTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg -90);
    }

    virtual public void Weapop_Flip(){
        if(joystick.Horizontal > 0) visualTransform.localScale = new Vector3(visualTransform.localScale.x,  -1, visualTransform.localScale.z);
        if(joystick.Horizontal < 0) visualTransform.localScale = new Vector3(visualTransform.localScale.x,  1, visualTransform.localScale.z);
    }

    virtual public void Hit(){
        // shoot continuously
        
        if(toggle.isOn && !isAttack){
            Debug.Log("hit");
            isAttack = true;
            weaponTransform.DOPunchPosition(weaponTransform.up * 0.5f, attackSpeed, 0, 0);
            bullet = bulletPooling.GetPooledObject();
            if(bullet == null) return;
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.GetComponent<Bullet>().dame = dame;
            bullet.GetComponent<Bullet>().MoveToDirection(weaponTransform.up, speed);
            StartCoroutine(Attack());
            // bulletPooling.RemovePooledObject(bullet);
            StartCoroutine(Remove(bullet));
            
        } 
        
    }

    virtual public IEnumerator Attack(){
        yield return new WaitForSeconds(attackSpeed);
        isAttack = false;
        
    }
    virtual public IEnumerator Remove(GameObject obj){
        yield return new WaitForSeconds(timeToDestroy);
        bulletPooling.RemovePooledObject(obj);
    }

    virtual public void UpgradeWeapon(int lvl){
        currentSprite = lvl;
        attackSpeed =  0.3f - 0.3f / maxLevel * level;
        if(attackSpeed <= 0.00001f) attackSpeed = 0.00001f;
        this.dame = lvl + 1;
        if(currentSprite >= gunSprite.Length) currentSprite = gunSprite.Length - 1;
        gunSpriteRenderer.sprite = gunSprite[currentSprite];
    }

    private void OnDisable() {
        isAttack = false;
    }
}
