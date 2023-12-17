using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerInfo : MonoBehaviour,ILevel
{
    public int health{get;set;}
    public int money = 0;
    public int score = 0;
    public int speed{get;set;}

    [Header("Health Effect")]
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private float timeEffect = 0.2f;
    [SerializeField]private GameObject shield;
    public event Action<object> OnPlayerReset;
     int levelHealth;
    int levelResilience;
    int levelShield;
    int levelSpeed;

    void Start()
    {
        health = 100;
        speed = 400;
        money = PlayerPrefs.GetInt("gold", 0);
        int levelHealth = PlayerPrefs.GetInt("health",0);
        int levelResilience = PlayerPrefs.GetInt("resilience",0);
        int levelShield = PlayerPrefs.GetInt("shield",0);
        int levelSpeed = PlayerPrefs.GetInt("speed",0);
        if(levelShield > 0){
            shield.SetActive(true);
        }

        health += levelHealth * 10;
        speed += levelSpeed * 10;
        // UIManager.instance.UpdateHealth(health);   
    }

    float duration = 1f;
    float time = 0f;
    private void Update() {
        time += Time.deltaTime;
        if (time >= duration){
            time = 0f;
            health += levelResilience;
        }
        
    }

    public void TakeDamage(int dame){
        dame -= dame * levelShield/10;
        health -= dame;
        // make the alpha of the sprite 0.5f
        spriteRenderer.DOFade(0.1f, timeEffect/2)
        .OnComplete(() => spriteRenderer.DOFade(0f, timeEffect/2));
        UIManager.instance.UpdateHealth(health);
        if(health <= 0){
            health = 0;
            Die();
            GameManager.instance.GameOver();
        }
    }


    public void Die(){
        
        Debug.Log("Die");
    }

    public void ResetPlayerInfo(){
        health = 100;
        speed = 300;
        money = PlayerPrefs.GetInt("gold", 0);
        OnPlayerReset?.Invoke(health);
    }
    
}
