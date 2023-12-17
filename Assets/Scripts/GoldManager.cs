using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{

    // Gold will increase depend on time survived 
    public float timeSurvived = 0f;
    public float timeFromStart = 0f;
    public static event Action OnGoldChange;

    [SerializeField] public int gold;

    private void Update() {
        timeFromStart += Time.deltaTime;
        timeSurvived += Time.deltaTime;
        if(timeSurvived >= 2f){
            // SoundManager.Play("coin");
            AddGold(1);
            timeSurvived = 0f;
        }
    }

    private void Start() {
        gold = PlayerPrefs.GetInt("gold",0);
        OnGoldChange?.Invoke();
    }
    void OnApplicationQuit(){
        SaveGold();
    }

    public void AddGold(int amount){
        OnGoldChange?.Invoke();
        gold += amount;
        
    }

    public void SaveGold(){
        PlayerPrefs.SetInt("gold", gold);
    }
    public void LoadGold(){
        gold = PlayerPrefs.GetInt("gold", 0);
    }
}
