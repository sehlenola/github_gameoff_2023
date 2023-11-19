using UnityEngine;

[CreateAssetMenu(fileName = "FireRateUpgrade", menuName = "WeaponUpgrades/FireRate")]
public class FireRateUpgrade : WeaponUpgrade
{
    public float fireRateMultiplier;

    public override void ApplyUpgrade(Weapon weapon)
    {
        weapon.GetWeaponDataInstance().weaponCooldown *= fireRateMultiplier;
    }
}