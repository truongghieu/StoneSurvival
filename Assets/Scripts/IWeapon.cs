
using UnityEngine;

public interface IWeapon
{   
    public abstract int Level { get; set; }

    public abstract void UpgradeWeapon(int level);

    public GameObject instance { get; set; }
}
