using UnityEngine;

[CreateAssetMenu(fileName = "PelletCountUpgrade", menuName = "WeaponUpgrades/PelletCount")]
public class PelletCountUpgrade : WeaponUpgrade
{
    public int additionalPellets;

    public override void ApplyUpgrade(Weapon weapon)
    {
        weapon.GetWeaponDataInstance().pelletCount += additionalPellets;
    }
}