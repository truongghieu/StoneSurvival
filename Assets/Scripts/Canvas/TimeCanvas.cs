using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCanvas : MonoBehaviour
{
    public Text timeText;
    public Text goldText;
    public GoldManager goldManager;

    void OnEnable(){
        GoldManager.OnGoldChange += UpdateGoldText;
    }

    private void Update() {
        ConvertToHHMMSS(goldManager.timeFromStart);
    }

    void ConvertToHHMMSS(float time){
        int hours = (int)time / 3600;
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    void UpdateGoldText(){
        goldText.text = goldManager.gold.ToString();
    }

    void OnDisable(){
        GoldManager.OnGoldChange -= UpdateGoldText;
    }

}
