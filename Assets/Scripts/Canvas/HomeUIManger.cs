using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HomeUIManger : MonoBehaviour
{
    [SerializeField] private Button SoundSetupButton;
    [SerializeField] private Button PlayGameButton;
    [SerializeField] private Button UpgradeButton;
    [SerializeField] private Button ExitButtonSound;
    [SerializeField] private Button ExitButtonUpgrade;
    [Header("Sound Volume")]
    [SerializeField] private Slider SoundVolumeSlider;

    [SerializeField] private GameObject SoundSettingPannel;

    [Header("Upgrade")]
    [SerializeField] private GameObject UpgradePannel;
    [SerializeField] private Button AcceptUpgradeButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Text UpgradeText;
    [SerializeField] private Text UpgradePriceText;
    [SerializeField] private Image UpgradeImage;
    [Space]
    [SerializeField] private Text GoldText;


    public delegate string OnAcceptUpgrade();
    
    public event OnAcceptUpgrade OnAcceptUpgradeButton;
    // create a string return event
    public delegate (string,string,Sprite) OnLeftButton();
    public event OnLeftButton OnLeftButtonEvent;
    public delegate (string,string,Sprite) OnRightButton();
    public event OnRightButton OnRightButtonEvent;
    public delegate (string,string,Sprite) UpgradePannelOpen();
    public event UpgradePannelOpen UpgradePannelOpenEvent;

    private void Awake()
    {
        Setup();
    }


    void Setup()
    {
        GoldText.text = PlayerPrefs.GetInt("gold",0).ToString();
        SoundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume",1);
        SoundSetupButton.onClick.AddListener(() =>{SoundManager.Play("click");});
        PlayGameButton.onClick.AddListener(() => {SoundManager.Play("click");});
        UpgradeButton.onClick.AddListener(() => {SoundManager.Play("click");});
        ExitButtonSound.onClick.AddListener(() => {SoundManager.Play("unpause");});
        ExitButtonUpgrade.onClick.AddListener(() => {SoundManager.Play("unpause");});
        AcceptUpgradeButton.onClick.AddListener(() => {SoundManager.Play("buy");});
        LeftButton.onClick.AddListener(() => {SoundManager.Play("click");});
        RightButton.onClick.AddListener(() => {SoundManager.Play("click");});
        AcceptUpgradeButton.onClick.AddListener(OnAcceptUpgradeButtonClicked);
        LeftButton.onClick.AddListener(OnLeftButtonClicked);
        RightButton.onClick.AddListener(OnRightButtonClicked);
        UpgradeButton.onClick.AddListener(OnUpgradePannelOpen);
        UpgradeButton.onClick.AddListener(() => {OpenUI(UIType.Upgrade);});
        SoundSetupButton.onClick.AddListener(() => {OpenUI(UIType.SoundSetting);});
        ExitButtonSound.onClick.AddListener(() => {CloseUI(UIType.SoundSetting);});
        SoundVolumeSlider.onValueChanged.AddListener(delegate {
            SoundManager.Volume(SoundVolumeSlider.value,track.All); 
            PlayerPrefs.SetFloat("SoundVolume",SoundVolumeSlider.value);
            });
        ExitButtonUpgrade.onClick.AddListener(() => {CloseUI(UIType.Upgrade);});
    }

    public void OpenUI(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.SoundSetting:
                SoundSettingPannel.SetActive(true);
                break;
            case UIType.Upgrade:
                UpgradePannel.SetActive(true);
                break;
            case UIType.PlayGame:
                break;
            default:
                break;
        }
    }

    public void CloseUI(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.SoundSetting:
                SoundSettingPannel.SetActive(false);
                break;
            case UIType.Upgrade:
                UpgradePannel.SetActive(false);
                break;
            case UIType.PlayGame:
                break;
            default:
                break;
        }
    }


    public void OnAcceptUpgradeButtonClicked()
    {
        Debug.Log("AcceptUpgradeButtonClicked");
        UpgradeText.text = OnAcceptUpgradeButton?.Invoke();
    }

    public void OnLeftButtonClicked()
    {
        Debug.Log("OnLeftButtonClicked");
        
        (string,string,Sprite)? data = OnLeftButtonEvent?.Invoke();
        if(data == null) return;
        UpgradeText.text = data.Value.Item1;
        UpgradePriceText.text = data.Value.Item2;
        UpgradeImage.sprite = data.Value.Item3;
        // string result = OnLeftButtonEvent?.Invoke();
        // Use the result variable as needed
    }

    public void OnRightButtonClicked()
    {
        Debug.Log("OnRightButtonClicked");  
        (string,string,Sprite)? data = OnRightButtonEvent?.Invoke();
        if(data == null) return;
        UpgradeText.text = data.Value.Item1;
        UpgradePriceText.text = data.Value.Item2;
        UpgradeImage.sprite = data.Value.Item3;
        // OnRightButtonEvent?.Invoke();
    }

    public void OnUpgradePannelOpen()
    {
        Debug.Log("OnUpgradePannelOpen");
        (string,string,Sprite)? data = UpgradePannelOpenEvent?.Invoke();
        if(data == null) return;
        UpgradeText.text = data.Value.Item1;
        UpgradePriceText.text = data.Value.Item2;
        UpgradeImage.sprite = data.Value.Item3;
        // UpgradePannelOpenEvent?.Invoke();
    }
}

[System.Serializable]
public enum UIType{
    SoundSetting,
    Upgrade,
    PlayGame
}
