using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponActivation : MonoBehaviour
{
    [Header("Type of Weapon")]
    public WeaponType weaponType;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            switch(weaponType){
                case WeaponType.AHWeapon:
                    other.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case WeaponType.KnifeWeapon:
                    other.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                    break;
                case WeaponType.Shield:
                    other.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
                    break;
                case WeaponType.GunWithMoreBullet:
                    other.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true);
                    break;
                case WeaponType.AreaWeapon:
                    other.transform.GetChild(1).transform.GetChild(5).gameObject.SetActive(true);
                    break;
                case WeaponType.SlowWeapon:
                    other.transform.GetChild(1).transform.GetChild(6).gameObject.SetActive(true);
                    break;
            }
            this.gameObject.SetActive(false);
        }
    }
}

public enum WeaponType{
    Gun,
    AHWeapon,
    KnifeWeapon,
    Shield,
    GunWithMoreBullet,
    AreaWeapon,
    SlowWeapon
}