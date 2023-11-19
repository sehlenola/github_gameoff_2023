using UnityEngine;

public abstract class WeaponUpgrade : ScriptableObject
{
    public string upgradeName;
    public Sprite upgradeSprite;
    public abstract void ApplyUpgrade(Weapon weapon);
}