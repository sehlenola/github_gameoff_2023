using UnityEngine;

[CreateAssetMenu(fileName = "AreaUpgrade", menuName = "WeaponUpgrades/Area")]
public class AreaUpgrade : WeaponUpgrade
{
    public float areaMultiplier;

    public override void ApplyUpgrade(Weapon weapon)
    {
        weapon.GetWeaponDataInstance().areaOfEffect *= areaMultiplier;
    }
}