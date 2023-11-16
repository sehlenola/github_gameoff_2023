using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private IWeapon currentWeapon;
    private ProjectileFactory factory;
    private List<IWeapon> allWeapons = new List<IWeapon>();
    private int currentWeaponIndex = 0;


    void Start()
    {
        // Create a ProjectileFactory instance
        factory = new ProjectileFactory();

        IWeapon machineGunWeapon = new WeaponMachineGun(factory, projectilePrefab, firePoint);
        IWeapon shotgunWeapon = new WeaponShotgun(factory, projectilePrefab, firePoint);

        allWeapons.Add(machineGunWeapon);
        allWeapons.Add(shotgunWeapon);

        IWeapon defaultWepon = allWeapons[currentWeaponIndex];
        SwitchWeapon(defaultWepon);

    }

    public void FireWeapon()
    {
        currentWeapon.Fire();
    }
    public void NextWeapon()
    {
        currentWeaponIndex++;
        currentWeaponIndex = currentWeaponIndex % allWeapons.Count;
        SwitchWeapon(allWeapons[currentWeaponIndex]);
    }
    public void SwitchWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
    public void UpgradeCurrentWeapon()
    {
        currentWeapon.Upgrade();
    }
}