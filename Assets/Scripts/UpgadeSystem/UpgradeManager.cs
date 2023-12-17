using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Ability List")]
    public List<Ability> abilities = new List<Ability>();
    [Space]
    [Header("UI Manager")]
    [SerializeField] private HomeUIManger HomeUIManger;
    [Space]

    int count = 0;
    int gold = 0;
    private void Start() {
        Setup();
    }
    public void Setup(){
        gold = PlayerPrefs.GetInt("gold",0);
        foreach(Ability abt in abilities){
            abt.AbilityMaxLevel = abt.AbilityPrice.Length - 1;
        }
        HomeUIManger.OnLeftButtonEvent += OnLeftButtonClicked;
        HomeUIManger.OnRightButtonEvent += OnRightButtonClicked;
        HomeUIManger.OnAcceptUpgradeButton += OnAcceptUpgradeButtonClicked;
        HomeUIManger.UpgradePannelOpenEvent += UpgradePannelOpenVoid;
    }

    private (string, string, Sprite) UpgradePannelOpenVoid()
    {
        int lvl = PlayerPrefs.GetInt(abilities[count].AbilityName, 0);
        return (abilities[count].AbilityDescription, abilities[count].AbilityPrice[lvl].ToString(), abilities[count].AbilityImage);
        throw new NotImplementedException();
    }

    private (string, string, Sprite) OnRightButtonClicked()
    {
        count = (count + 1) % abilities.Count;
        int lvl = PlayerPrefs.GetInt(abilities[count].AbilityName, 0);
        return (abilities[count].AbilityDescription, abilities[count].AbilityPrice[lvl].ToString(), abilities[count].AbilityImage);
        throw new NotImplementedException();
    }

    private (string, string, Sprite) OnLeftButtonClicked()
    {
        count = (count - 1) % abilities.Count;
        if (count < 0) count = abilities.Count - 1;
        int lvl = PlayerPrefs.GetInt(abilities[count].AbilityName, 0);
        return (abilities[count].AbilityDescription, abilities[count].AbilityPrice[lvl].ToString(), abilities[count].AbilityImage);
    }
    private string OnAcceptUpgradeButtonClicked()
    {
        int lvl = PlayerPrefs.GetInt(abilities[count].AbilityName, 0);
        if (gold >= abilities[count].AbilityPrice[lvl] && lvl < abilities[count].AbilityMaxLevel)
        {
            gold -= abilities[count].AbilityPrice[lvl];
            PlayerPrefs.SetInt("gold", gold);
            lvl++;
            PlayerPrefs.SetInt(abilities[count].AbilityName, lvl);
            return "Upgrade Success";
        }
        else if (lvl >= abilities[count].AbilityMaxLevel)
        {
            return "Max Level";
        }
        else
        {
            return "Not Enough Gold";
        }
        throw new NotImplementedException();
    }
}
