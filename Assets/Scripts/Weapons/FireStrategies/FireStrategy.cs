using UnityEngine;

public abstract class FireStrategy : ScriptableObject
{
    public abstract void Fire(Transform firePoint, WeaponData weaponData);
}