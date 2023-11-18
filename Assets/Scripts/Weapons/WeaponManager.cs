using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject weaponHolder;

    private List<Weapon> allNewWeapons = new List<Weapon>();

    [SerializeField] List<WeaponData> weapons = new List<WeaponData>();



    void Start()
    {

        AddWeapon(weapons[0]);
        AddWeapon(weapons[1]);

    }

    private void Update()
    {
        foreach(var weapon in allNewWeapons)
        {
            weapon.UpdateWeaponTimer(Time.deltaTime, 1f);
        }
    }


    public void AddWeapon(WeaponData weaponData)
    {
        Weapon existingWeapon = weaponHolder.GetComponents<Weapon>().FirstOrDefault(w => w.weaponData == weaponData);
        if(existingWeapon != null)
        {
            return;
        }

        Weapon newWeapon = weaponHolder.AddComponent<Weapon>();
        newWeapon.SetupWeapon(weaponData);
        allNewWeapons.Add(newWeapon);
    }
}