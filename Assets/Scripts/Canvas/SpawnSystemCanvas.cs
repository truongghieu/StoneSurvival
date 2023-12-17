using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
public class SpawnSystemCanvas : MonoBehaviour
{
    [Header("Spawn System Canvas")]
    [SerializeField] private Text roundText;

    [SerializeField] private SpawnSystem spawnSystem;


    private void OnEnable() {
        SpawnSystem.OnSpawnStart += OnSpawnStartText;
    }

    void Start()
    {
        roundText.color = new Color(1f, 1f, 1f, 0f);
    }

    public void OnSpawnStartText(){
        // make the text scale to 1.5x and back to 1x and fade out
        roundText.DOFade(1f, 0.5f);
        roundText.text = "Wave " + spawnSystem.round.ToString();
        roundText.transform.DOScale(1.5f, 0.5f).OnComplete(() => {
            roundText.transform.DOScale(1f, 0.5f).OnComplete(() => {
                roundText.DOFade(0f, 0.5f);});
        });
    }


    private void OnDisable() {
        SpawnSystem.OnSpawnStart -= OnSpawnStartText;
    }
}
