using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeaponMachineGun : IWeapon
{
    public float BaseDamage { get; set; } = 10f;

    public WeaponData WeaponData { get; set; }

    private IProjectileFireStrategy fireStrategy;
    private GameObject projectilePrefab;
    private Transform firePoint;

    public WeaponMachineGun(ProjectileFactory factory, GameObject prefab, Transform firePt, WeaponData weaponData)
    {
        fireStrategy = new MachineGunFireStrategy(factory);
        projectilePrefab = prefab;
        firePoint = firePt;
        this.WeaponData = weaponData;
    }

    public void Fire()
    {
        fireStrategy.Fire(projectilePrefab, firePoint.position, firePoint.rotation);

        // Randomly select a sound
        AudioClip randomSound = WeaponData.fireSounds[Random.Range(0, WeaponData.fireSounds.Length)];
        // Play the sound at the weapon's position
        AudioSource.PlayClipAtPoint(randomSound, firePoint.position);
    }

    public void Upgrade()
    {
        // Upgrade logic for Machine Gun
        fireStrategy.PelletsCount++;
    }
}
