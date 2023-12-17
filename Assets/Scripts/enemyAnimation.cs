using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimation : MonoBehaviour
{
    public Animation anim;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;


    private void Start() {
        anim = GetComponent<Animation>();

        var clip = new AnimationClip();
        var curve = AnimationCurve.Linear(0, 0, 1, 1);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
        // add sprite1
        clip.AddEvent(new AnimationEvent() {
            time = 0,
            functionName = "ChangeSprite",
            stringParameter = "sprite1"
        });
        // add sprite2
        clip.AddEvent(new AnimationEvent() {
            time = 0.25f,
            functionName = "ChangeSprite",
            stringParameter = "sprite2"
        });
        // add sprite3
        clip.AddEvent(new AnimationEvent() {
            time = 0.5f,
            functionName = "ChangeSprite",
            stringParameter = "sprite3"
        });
        // add sprite4
        clip.AddEvent(new AnimationEvent() {
            time = 0.75f,
            functionName = "ChangeSprite",
            stringParameter = "sprite4"
        });
        anim.AddClip(clip, "clip");
        anim.Play("clip");
        

    }

}
