using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HPoolingObject;
using System;

public class Enemy : MonoBehaviour,IObject
{
    public Transform target;
    public float speed = 5f;
    public float attackRange = 1f;
    public int health = 100;
    private int h = 0;
    public int dame = 10;
    [field: SerializeField]
    public string UniqueID { get; set; }

    private Action<IObject> _returnAction;

    [Header("Hit Effect")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private Color SlowColor = Color.blue;

    void Start(){
        Setup();
        
    }

    public void Init(Action<IObject> returnToPool){
        _returnAction = returnToPool;
    }

    public void returnToPool(){
        _returnAction?.Invoke(this);
    }
    void Update(){
        
        Move(); 
        Flip();
    }


    virtual public void Move(){
        if(Vector2.Distance(transform.position, target.position) < attackRange) return;
        this.transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    virtual public void Flip(){
        if(target.position.x - transform.position.x > 0){
            transform.GetChild(0).localScale = new Vector3(1,1,1);
        }else if(target.position.x - transform.position.x < 0){
            transform.GetChild(0).localScale = new Vector3(-1,1,1);
        }
    }


    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "PlayerBullet"){
            HitEffect();
        }
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerInfo>().TakeDamage(dame);
            returnToPool();
        }
    }

    void Setup(){
        h = health;
        target = GameObject.Find("Player").transform;
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void HitEffect(){
        sr.color = hitColor;
        Invoke("ResetColor", 0.1f);
    }
    void ResetColor(){
        sr.color = Color.white;
    }
    public void TakeDamage(int dame){
        this.h -= dame;
        if(h <= 0){
            returnToPool();
            GetComponent<EnemySpawnMana>().SpawnMana();
        }
    }

    public void Slow(float slowPercent){
        this.speed -= speed * slowPercent;
        sr.color = SlowColor;
    }

    
}
