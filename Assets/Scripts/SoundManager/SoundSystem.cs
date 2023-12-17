using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{

    public static SoundSystem instance;

    private void Start() {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    public void PlaySound(string soundName)
    {
        SoundManager.Play(soundName);
    }    

    
}
