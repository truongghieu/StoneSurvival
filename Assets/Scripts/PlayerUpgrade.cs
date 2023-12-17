using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade 
{

    public string GenerateText(int a){

        switch (a)
        {
            case 0:
                return "Upgrade your HP";
            case 1:
                return "Upgrade your Speed";
            case 2:
                return "Upgrade your Gun";
            case 3:
                return "Upgrade your Rocket";
            case 4:
                return "Upgrade your Knife";
            case 5:
                return "Upgrade your Shot gun";
            case 6:
                return "Upgrade your Sharingan";
            case 7:
                return "Upgrade your Rasengan Gun";
            default:
                return "I dont know ?";
        }

    }

    // 
    public void UpgradeLevelWeapon<T>(T weapon) where T : IWeapon
    {
        if(weapon is IWeapon s){
            s.Level +=1;
        }else{
            Debug.Log($"Wrong type of weapon {weapon.GetType()}");
        }
    }
    public void HealthUpgrade<T>(T l,int n) where T : ILevel{
        if(n == 0){
            l.health += 5;
        }else if(n == 1) l.speed += 10;
    }
}
