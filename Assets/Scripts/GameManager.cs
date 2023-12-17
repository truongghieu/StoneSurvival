using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] private GoldManager goldManager;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private UIManager uIManager;

    public static event Action<object> OnGameStart;
    public static event Action OnGameOver;


    private void Start() {
        
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.sceneLoaded += setupForNewScene;
        if(uIManager == null) return;
            
        
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= setupForNewScene;
        if (uIManager == null) return;
        uIManager.OnGameHomeCanvas -= Home;
        uIManager.OnGameRestartCanvas -= Restart;
    }

    void setupForNewScene(Scene scene, LoadSceneMode mode){
        goldManager = FindObjectOfType<GoldManager>();
        playerInfo = FindObjectOfType<PlayerInfo>();
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager == null) return;
        uIManager.OnGameHomeCanvas += Home;
        uIManager.OnGameRestartCanvas += Restart;
    }

    public void StartGame(){
        goldManager.LoadGold();
        OnGameStart?.Invoke(null);
    }

    public void GameOver(){
        OnGameOver?.Invoke();
        goldManager.SaveGold();
    }

    public void Restart(){
        goldManager.SaveGold();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart");
    }


    
    public void Home(){
        goldManager.SaveGold();
        SceneManager.LoadScene("Home");
    }

    public void LoadScene(string sceneName){
        goldManager.LoadGold();
        SceneManager.LoadScene(sceneName);
    }
}
