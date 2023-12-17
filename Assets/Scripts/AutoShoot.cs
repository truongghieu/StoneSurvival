using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private PoolingObject BulletPooling;
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletLifeTime = 1f;
    [SerializeField] private Transform bulletSpawnPoint;
    private GameObject bullet;
    private GameObject target;
    private bool isAttack = false;

    void Start(){
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        AutoShootBullet();
    }

    virtual public void AutoShootBullet(){
        if(isAttack) return;
        bullet = BulletPooling.GetPooledObject();
        if(bullet == null) return;
        isAttack = true;
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Bullet>().MoveToDirection((Vector2)(target.transform.position - bulletSpawnPoint.position).normalized, bulletSpeed);
        StartCoroutine(Remove(bullet));
        StartCoroutine(wait());
    }

    virtual public IEnumerator Remove(GameObject obj){
        yield return new WaitForSeconds(bulletLifeTime);
        BulletPooling.RemovePooledObject(obj);
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(shootDelay);
        isAttack = false;
    }
}
