using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb; 
    public Joystick js;
    private Animator anim;
    private SuperPowerGun weapon;
    public float speed = 5f;

    void Start(){
        Setup();
        
    }

    void Update(){
        Move();
    }

    public void Move(){ 
        
        // if(weapon.isAttack){
        //     rb.velocity = Vector2.zero;
        //     return;
        // }
        // flip and animation
        if(js.Horizontal > 0 ){
            anim.SetBool("running",true);
            transform.GetChild(0).localScale = new Vector3(1,1,1);
        }else if(js.Horizontal < 0){
            transform.GetChild(0).localScale = new Vector3(-1,1,1);
            anim.SetBool("running",true);
        }else {
            anim.SetBool("running",false);
        }
        // movement
        float x = Mathf.Clamp(Mathf.RoundToInt(js.Horizontal),-1,1);
        float y = Mathf.Clamp(Mathf.RoundToInt(js.Vertical),-1,1);
        x = x / Mathf.Sqrt(x*x + y*y);
        y = y / Mathf.Sqrt(x*x + y*y);
        // rb.velocity = new Vector2(js.Horizontal * speed * Time.deltaTime , js.Vertical * speed * Time.deltaTime);
        if(double.IsNaN(x) || double.IsNaN(y)){
            rb.velocity = new Vector2(0,0);
            return;
        }
        rb.velocity = new Vector2(x * speed * Time.deltaTime , y * speed * Time.deltaTime);
        
        
    }


    void Setup(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<SuperPowerGun>();
    }
    
}
