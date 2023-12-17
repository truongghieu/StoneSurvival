using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeWeapon : MonoBehaviour,IWeapon
{
    private RotateC rotateC;
    [SerializeField] public int level = 0;
    [SerializeField] public int maxLevel = 7;
    public int dame = 1;
    public GameObject instance { get; set; }

    public int Level{
        get{return level;}
        set{
            level = value;
            UpgradeWeapon(level);
        }
    }
    [SerializeField] private Sprite[] knifeSprite;

    private int currentSprite = 0;
    [SerializeField] private SpriteRenderer knifeSpriteRenderer;

    private void Awake() {
        instance = this.gameObject;
    }
    void Start()
    {
        Setup();
    }
    void Setup(){
        rotateC = GetComponent<RotateC>();
        Level = PlayerPrefs.GetInt("KnifeLevel", 0);
        

    }   
    public void UpgradeWeapon(int lvl){
        currentSprite = lvl;
        rotateC.speed = 200 + lvl * 50; 
        if(rotateC.speed > 1000) rotateC.speed = 1000;
        dame = 1 + lvl;
        if(currentSprite > knifeSprite.Length - 1) {
            knifeSpriteRenderer.sprite = knifeSprite[knifeSprite.Length-1];
            return;
        } 
        knifeSpriteRenderer.sprite = knifeSprite[currentSprite];
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().TakeDamage(dame);
        }
    }
}
