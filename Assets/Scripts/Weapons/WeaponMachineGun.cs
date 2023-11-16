using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeaponMachineGun : IWeapon
{
    public float BaseDamage { get; set; } = 10f;
    private IProjectileFireStrategy fireStrategy;
    private GameObject projectilePrefab;
    private Transform firePoint;

    public WeaponMachineGun(ProjectileFactory factory, GameObject prefab, Transform firePt)
    {
        fireStrategy = new MachineGunFireStrategy(factory);
        projectilePrefab = prefab;
        firePoint = firePt;
    }

    public void Fire()
    {
        fireStrategy.Fire(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void Upgrade()
    {
        // Upgrade logic for Machine Gun
        fireStrategy.PelletsCount++;
    }
}
