using UnityEngine;
using static Cinemachine.CinemachineStoryboard;

public class WeaponShotgun: IWeapon
{
    public float BaseDamage { get; set; } = 10f;
    public WeaponData WeaponData { get; set; }
    private ShotgunFireStrategy fireStrategy;
    public GameObject projectilePrefab;
    public Transform firePoint;


    public WeaponShotgun(ProjectileFactory factory, GameObject prefab, Transform firePt, WeaponData weaponData)
    {
        fireStrategy = new ShotgunFireStrategy(factory, 3, 10f);
        projectilePrefab = prefab;
        firePoint = firePt;
        WeaponData = weaponData;
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
        // Upgrade logic for Shotgun
        BaseDamage += 5; 
        fireStrategy.PelletsCount += 1;
        fireStrategy.SpreadAngle += 15f;
    }
}