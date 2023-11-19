using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "WeaponUpgrades/Damage")]
public class DamageUpgrade : WeaponUpgrade
{
    public float additionalDamage;

    public override void ApplyUpgrade(Weapon weapon)
    {
        weapon.GetWeaponDataInstance().weaponDamage += additionalDamage;
    }
}