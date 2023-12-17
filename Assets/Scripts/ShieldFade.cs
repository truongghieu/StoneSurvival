using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShieldFade : MonoBehaviour
{
    [SerializeField]private float timeEffect = 1f;
    [SerializeField]private float alpaColor1 = 0.5f;


    private SpriteRenderer spriteRenderer;

    float tempTime;

    bool isFade = true;
    void Start(){
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        StartEffect();
    }
    void Update()
    {
        
    }

    void StartEffect(){
        // Yoyo effect
        spriteRenderer.DOFade(alpaColor1, timeEffect).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

    }

}
