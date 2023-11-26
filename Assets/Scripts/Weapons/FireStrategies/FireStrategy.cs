using UnityEngine;

public abstract class FireStrategy : ScriptableObject
{
    public abstract void Fire(Transform firePoint, WeaponData weaponData);
    public void PlayFireSound(WeaponData weaponData, Transform soundPosition)
    {
        if (weaponData.fireSounds.Length > 0)
        {
            AudioSource.PlayClipAtPoint(weaponData.fireSounds[Random.Range(0,weaponData.fireSounds.Length)], soundPosition.position);
        }
    }
}