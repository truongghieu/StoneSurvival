using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayergold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("gold", 1000000);
    }

   
}
