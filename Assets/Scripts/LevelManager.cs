using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    static public LevelManager instance;

    public static LevelManager Instance { get => instance; set => instance = value; }
    
    public int level = 0;
    public int[] expLevel;
    public int exp;

    
    public static event Action<object> OnLevelUp;
    public static event Action<object> OnAddExp;


    // init exp level
    private void Awake() {
        expLevel = new int[100];
        expLevel[0] = 5;
        expLevel[1] = 10;
        expLevel[2] = 15;
        for(int i = 3; i < 100; i++){
            // create a list that increase 30% each level
            if (expLevel[i-1] > 2000){
                expLevel[i] = (int) (expLevel[i-1] * 1.05);
                continue;
            }
            expLevel[i] = (int) (expLevel[i-1] * 1.3);
        }
    }

    private void Start() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        }

        AddExp(0);
    }

    public void AddExp(int amount){
        exp += amount;
        OnAddExp?.Invoke((float) exp / expLevel[level]);
        if(exp >= expLevel[level]){
            SoundManager.Play("powerUp");
            level++;
            exp = 0;
            OnLevelUp?.Invoke(2);
            // UIManager.instance.Btn1.onClick.AddListener(
        }
    }




}

