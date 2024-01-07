using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    private void Start() {
        Setup();
    }

    [Header("Mana Slider")]

    [SerializeField] public Image ManaSlider;
    [Header("Update Panel")]
    [SerializeField] private GameObject updatePanel;
    [SerializeField] public Text textUpdate_1;
    [SerializeField] public Text textUpdate_2;
    [SerializeField] public Text textUpdate_3;

    [SerializeField] public Button Btn1;
    [SerializeField] public Button Btn2;
    [SerializeField] public Button Btn3;

    [Header("Player Info")]
    [SerializeField] public PlayerInfo playerinfo;

    [SerializeField] private Text goldText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image avatar;

    [Header("Reference Weapon")]

    [SerializeField] public SuperPowerGun Gunn;
    [SerializeField] public RocketWeapon AHWeapon;
    [SerializeField] public KnifeWeapon KnifeWeapon;
    [SerializeField] public GunWithMoreBullets GunWithMoreBullet;
    [SerializeField] public Sharingan EreaWeapon;
    [SerializeField] public SlowWeapon SlowWeapon;

    [Header("Player Info")]
    [SerializeField] public PlayerInfo playerInfo;


    PlayerUpgrade playerUpgrade = new PlayerUpgrade();
    

    [Header("Game Pause Panel")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button closeButton;

    public event Action OnGameRestartCanvas;
    public event Action OnGameHomeCanvas;

    int a,b,c;

    private void OnEnable() {
        playerInfo.OnPlayerReset += UpdateHealth;
        LevelManager.OnAddExp += UpdateMana;
        LevelManager.OnLevelUp += LevelUpgradeHandler;
        GameManager.OnGameStart += UpdateGold;
        GameManager.OnGameOver += GameOverHandler;
    }

    private void GameOverHandler()
    {
        SoundManager.Play("die");
        Time.timeScale = 0;
        closeButton.interactable = false;
        pausePanel.SetActive(true);

    }

    private void OnDisable() {
        LevelManager.OnAddExp -= UpdateMana;
        LevelManager.OnLevelUp -= UpdateLevel;
        LevelManager.OnLevelUp -= LevelUpgradeHandler;
        playerInfo.OnPlayerReset -= UpdateHealth;
        GameManager.OnGameStart -= UpdateGold;
        GameManager.OnGameOver -= GameOverHandler;
    }

    public void UpdateGold(object amount){
        // update gold
        // GoldManager.Instance.AddGold(1);
        if (amount is int amountInt){
            goldText.text = amountInt.ToString();
        }else{
            Debug.Log($"Unexpected type {amount.GetType()}");
        }
    }
    
    public void GamePauseHandler(){
        Debug.Log("Pause from UIManager");
        SoundManager.Play("pause");
        Time.timeScale = 0;
        OpenPannel(pausePanel);
    }

    void GameResumeHandler(){
        SoundManager.Play("unpause");
        Time.timeScale = 1;
        ClosePanel(pausePanel);
    }



    public void UpdateMana(object amount){
        // update mana,type casting
        if(amount is float amountFloat){
        ManaSlider.fillAmount = amountFloat;
        }else{
            Debug.Log($"Unexpected type {amount.GetType()}");
        }
    }

    public void UpdateLevel(object parameter){
        // update level
        textUpdate_1.text = "text1";
        textUpdate_2.text = "text2";
        textUpdate_3.text = "text3";
        updatePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdateHealth(object amount){
        // update health
        if(amount is int amountInt){
            healthBar.fillAmount = amountInt / 100f;
    }else{
        Debug.Log($"Unexpected type {amount.GetType()}");
    }
    }

    void Setup(){
        instance = this;
    }

    

    public void LevelUpgradeHandler(object parameter){
        Time.timeScale = 0;
        a = UnityEngine.Random.Range(0,8);
        // random b != a
        b = UnityEngine.Random.Range(0,8);
        while(b == a){
            b = UnityEngine.Random.Range(0,8);
        }
        // random c != a,b
        c = UnityEngine.Random.Range(0,8);
        while(c == a || c == b){
            c = UnityEngine.Random.Range(0,8);
        }
        
        textUpdate_1.text = playerUpgrade.GenerateText(a);
        textUpdate_2.text = playerUpgrade.GenerateText(b);
        textUpdate_3.text = playerUpgrade.GenerateText(c);
        ButtonAddListener(Btn1,a);
        ButtonAddListener(Btn2,b);
        ButtonAddListener(Btn3,c);
        updatePanel.SetActive(true);
    }


    void ButtonAddListener(Button btn,int type){
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => {SoundManager.Play("buy");});
        btn.onClick.AddListener(ExitPannel);
        switch(type){
            case 0:
                // add listener to btn
                btn.onClick.AddListener(() => {playerUpgrade.HealthUpgrade(playerinfo,0);});
                break;
            case 1:
                btn.onClick.AddListener(() => {playerUpgrade.HealthUpgrade(playerinfo,1);});
                break;
            case 2:
                if (Gunn.gameObject.activeSelf == false){
                    btn.onClick.AddListener(() => {Gunn.gameObject.SetActive(true);});
                    }else{
                    btn.onClick.AddListener(() => {playerUpgrade.UpgradeLevelWeapon(Gunn);});
                }
                break;
            case 3:
                if (AHWeapon.gameObject.activeSelf == false){
                    btn.onClick.AddListener(() => {AHWeapon.gameObject.SetActive(true);});
                    }else{
                    btn.onClick.AddListener(() => {playerUpgrade.UpgradeLevelWeapon(AHWeapon);});
                }
                break;
            case 4:
                if (KnifeWeapon.gameObject.activeSelf == false){
                    btn.onClick.AddListener(() => {KnifeWeapon.gameObject.SetActive(true);});
                    }else{
                    btn.onClick.AddListener(() => {playerUpgrade.UpgradeLevelWeapon(KnifeWeapon);});
                }
                break;
            case 5:
                if (GunWithMoreBullet.gameObject.activeSelf == false){
                    btn.onClick.AddListener(() => {GunWithMoreBullet.gameObject.SetActive(true);});
                    }else{
                    btn.onClick.AddListener(() => {playerUpgrade.UpgradeLevelWeapon(GunWithMoreBullet);});
                }
                break;
            case 6:
                if (EreaWeapon.gameObject.activeSelf == false){
                    btn.onClick.AddListener(() => {EreaWeapon.gameObject.SetActive(true);});
                    }else{
                    btn.onClick.AddListener(() => {playerUpgrade.UpgradeLevelWeapon(EreaWeapon);});
                }
                break;
            case 7:
                if (SlowWeapon.gameObject.activeSelf == false){
                    btn.onClick.AddListener(() => {SlowWeapon.gameObject.SetActive(true);});
                    }else{
                    btn.onClick.AddListener(() => {playerUpgrade.UpgradeLevelWeapon(SlowWeapon);});
                }
                break;
            default:
                break;
        }
    }

    public void ExitPannel(){
        SoundManager.Play("unpause");
        updatePanel.SetActive(false);
        Time.timeScale = 1;
        Btn1.onClick.RemoveAllListeners();
        Btn2.onClick.RemoveAllListeners();
        Btn3.onClick.RemoveAllListeners();
    }

    public void ClosePanel(GameObject pannel){
        SoundManager.Play("click");
        pannel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenPannel(GameObject pannel){
        SoundManager.Play("click");
        pannel.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Restart(){
        closeButton.interactable = true;
        SoundManager.Play("click");
        ClosePanel(pausePanel);
        OnGameRestartCanvas?.Invoke();
    }
    public void Home(){
        closeButton.interactable = true;
        SoundManager.Play("click");
        ClosePanel(pausePanel);
        OnGameHomeCanvas?.Invoke();
    }
    
}
