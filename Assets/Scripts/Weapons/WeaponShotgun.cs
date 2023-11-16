using UnityEngine;
using static Cinemachine.CinemachineStoryboard;

public class WeaponShotgun: IWeapon
{
    public float BaseDamage { get; set; } = 10f;
    private ShotgunFireStrategy fireStrategy;
    public GameObject projectilePrefab;
    public Transform firePoint;


    public WeaponShotgun(ProjectileFactory factory, GameObject prefab, Transform firePt)
    {
        fireStrategy = new ShotgunFireStrategy(factory, 3, 10f);
        projectilePrefab = prefab;
        firePoint = firePt;
    }

    public void Fire()
    {
        
        fireStrategy.Fire(projectilePrefab, firePoint.position, firePoint.rotation);
        
    }

    public void Upgrade()
    {
        // Upgrade logic for Shotgun
        BaseDamage += 5; // Example upgrade increment
        fireStrategy.PelletsCount += 1; // Example increment
        fireStrategy.SpreadAngle += 15f; // Example increment
    }
}