using UnityEngine;
using static Cinemachine.CinemachineStoryboard;

public class WeaponShotgun: IWeapon
{
    public float BaseDamage { get; set; } = 10f;
    public WeaponData WeaponData { get; set; }
    public WeaponData WeaponDataInstance { get; set; }
    public float CooldownTime { get; private set; } = 0.5f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public void Fire()
    {

        WeaponDataInstance.fireStrategy.Fire(firePoint, WeaponDataInstance);

        AudioClip randomSound = WeaponData.fireSounds[Random.Range(0, WeaponDataInstance.fireSounds.Length)];
        AudioSource.PlayClipAtPoint(randomSound, firePoint.position);

    }

    public void Upgrade()
    {
        // Upgrade logic for Shotgun
        BaseDamage += 5;
        WeaponDataInstance.pelletCount += 1;
    }

    public void UpdateWeaponCooldown(float timeDelta, float cooldownMultiplier)
    {
        CooldownTime -= timeDelta * cooldownMultiplier;
        if (CooldownTime <= 0)
        {
            Fire();
            CooldownTime = WeaponDataInstance.weaponCooldown;
        }
    }
}