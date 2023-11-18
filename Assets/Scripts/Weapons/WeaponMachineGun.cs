using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeaponMachineGun : IWeapon
{
    public float BaseDamage { get; set; } = 10f;

    public WeaponData WeaponData { get; set; }
    public WeaponData WeaponDataInstance { get; set; }
    public float CooldownTime { get; private set; } = 0.5f;


    private Transform firePoint;



    public void Fire()
    {
        WeaponDataInstance.fireStrategy.Fire(firePoint, WeaponDataInstance);

        AudioClip randomSound = WeaponData.fireSounds[Random.Range(0, WeaponDataInstance.fireSounds.Length)];
        AudioSource.PlayClipAtPoint(randomSound, firePoint.position);
    }

    public void Upgrade()
    {
        WeaponDataInstance.weaponCooldown *= .9f;
    }

    public void UpdateWeaponCooldown(float timeDelta, float cooldownMultiplier)
    {
        CooldownTime -= timeDelta * cooldownMultiplier;
        if(CooldownTime <= 0)
        {
            Fire();
            CooldownTime = WeaponDataInstance.weaponCooldown;
        }
    }
}
