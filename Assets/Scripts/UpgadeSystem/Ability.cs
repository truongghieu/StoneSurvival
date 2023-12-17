using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{   
    public string AbilityName;
    public string AbilityDescription;
    public Sprite AbilityImage;
    public int[] AbilityPrice;
    public int AbilityLevel;
    public int AbilityMaxLevel;

}
