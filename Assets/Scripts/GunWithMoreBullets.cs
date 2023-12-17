using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GunWithMoreBullets : SuperPowerGun
{   

    public int bulletAmount = 1;
    public int angleRange = 60;
    public Vector3 RotateDirection;
    public float rotateSpeed = 200f;

    private int currentSprite = 0;

    override public void Hit(){
        // shoot continuously
        
        if(!isAttack){
            // Debug.Log("hit");
            isAttack = true;
            weaponTransform.DOPunchPosition(weaponTransform.up * 0.5f, attackSpeed/10, 0, 0);
            // bullet = bulletPooling.GetPooledObject();
            // if(bullet == null) return;
            // bullet.transform.position = bulletSpawnPoint.position;
            // bullet.GetComponent<Bullet>().MoveToDirection(weaponTransform.up, speed);
            
            // shoot with more bullet
            for(int i = 0; i < bulletAmount; i++){
                bullet = bulletPooling.GetPooledObject();
                if(bullet == null){
                    isAttack = false;
                    return;
                }
                bullet.transform.position = bulletSpawnPoint.position;
                // weaponTransform.rotation = Quaternion.Euler(0, 0, Random.Range(weaponTransform.rotation.eulerAngles.z - angleRange/2, weaponTransform.rotation.eulerAngles.z + angleRange/2));
                Vector3 dir = Quaternion.AngleAxis(Random.Range(-angleRange/2, angleRange/2), Vector3.forward) * weaponTransform.up;
                bullet.GetComponent<Bullet>().MoveToDirection(dir, speed);
                StartCoroutine(Remove(bullet));
                StartCoroutine(Attack());
                StartCoroutine(Remove(bullet));
                StartCoroutine(Cowndown(attackSpeed));
            }
        
            
        } 
        
    }
    IEnumerator Cowndown(float time){
        yield return new WaitForSeconds(time);
        isAttack = false;
    }
    override public void Rotate(){
        // Debug.Log("rotate");
        weaponTransform.Rotate(RotateDirection * rotateSpeed * Time.deltaTime);
    }

    override public void UpgradeWeapon(int lvl){
        currentSprite = lvl;
        attackSpeed = (0.2f - (Level * 0.01f));
        if(attackSpeed < 0.01f) attackSpeed = 0.01f;
        if(currentSprite >= gunSprite.Length) currentSprite = gunSprite.Length - 1;
        bulletAmount = lvl + 1;
        rotateSpeed = 200f + (lvl * 50f);
        if(rotateSpeed > 500f) rotateSpeed = 500f;
        gunSpriteRenderer.sprite = gunSprite[currentSprite];
    }
}
