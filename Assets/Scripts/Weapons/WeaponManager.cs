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

    [SerializeField] List<WeaponData> weapons = new List<WeaponData>();


    void Start()
    {
        // Create a ProjectileFactory instance
        factory = new ProjectileFactory();

        IWeapon machineGunWeapon = new WeaponMachineGun(factory, projectilePrefab, firePoint, weapons[0]);
        IWeapon shotgunWeapon = new WeaponShotgun(factory, projectilePrefab, firePoint, weapons[1]);

        allWeapons.Add(machineGunWeapon);
        allWeapons.Add(shotgunWeapon);

        IWeapon defaultWepon = allWeapons[currentWeaponIndex];
        SwitchWeapon(defaultWepon);

    }
    private void Update()
    {
        foreach(var weapon in allWeapons)
        {
            weapon.UpdateWeaponCooldown(Time.deltaTime, 1f);
        }
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