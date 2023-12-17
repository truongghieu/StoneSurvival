using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sharingan : MonoBehaviour,IWeapon
{
    [Header("Weapon Stats")]
    [SerializeField]private float attackSpeed = 0.5f;
    [SerializeField]private float attackRange = 1.5f;
    [SerializeField]private int dame = 1;
    [Header("Weapon Level")]
    private int level = 0;
    [SerializeField] public int maxLevel = 5;
    public PoolingObject bulletPool;
    [SerializeField] private float timeToDestroy = 1f;
    [SerializeField] private Color Color_0;
    [SerializeField] private Color Color_1;
    [SerializeField] private float randomSize_0 = 0.5f;
    [SerializeField] private float randomSize_1 = 1.5f;

    [SerializeField] Sprite[] sprites;
    private int currentSprite = 0;
    public GameObject instance { get; set; }

    private GameObject bullet;

    public int Level{
        get{
            return level;
        }
        set{
            level = value;
            UpgradeWeapon(level);
        }
    }
     private bool isAttacking = false;

    void Awake(){
        instance = this.gameObject;
    }

    void Update(){
        Attack();
    }

    virtual public void Attack(){
        if(isAttacking) return;
        isAttacking = true;
        StartCoroutine(waitForNextAttack());
        Vector2 dir = Random.insideUnitCircle;
        // dir.Normalize();
        bullet = bulletPool.GetPooledObject();
        if(bullet == null) return;
        bullet.GetComponent<EreaBullet>().dame = dame;
        bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0) * attackRange;
        // random gradient from Color_0 to Color_1
        bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color_0, Color_1, Random.Range(0f, 1f));
        bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        // random size from randomSize_0 to randomSize_1
        bullet.transform.GetChild(0).transform.localScale = Vector3.one * Random.Range(randomSize_0, randomSize_1);
        bulletPool.RemovePooledObject(bullet, timeToDestroy); 

    }


    IEnumerator waitForNextAttack(){
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }


    virtual public void UpgradeWeapon(int lvl){
        currentSprite = lvl;
        if ( currentSprite >= sprites.Length ) currentSprite = sprites.Length - 1;
        if (attackSpeed <= 0.001f) attackSpeed = 0.001f;
        attackSpeed = 0.5f  -  0.01f * lvl;
        if(attackSpeed < 0.05f) attackSpeed = 0.05f;
        dame = 1 + 1 * lvl;
        attackRange = 1.5f + 0.2f * lvl;
        if(attackRange > 3f) attackRange = 3f;
    }
    private void OnDisable() {
        isAttacking = false;
    }
}
