using UnityEngine;

public abstract class FireStrategy : ScriptableObject
{
    public abstract void Fire(Transform firePoint, WeaponData weaponData);
    public void PlayFireSound(WeaponData weaponData, Transform soundPosition, float volume)
    {
        if (weaponData.fireSounds.Length > 0)
        {
            //AudioSource.PlayClipAtPoint(weaponData.fireSounds[Random.Range(0,weaponData.fireSounds.Length)], soundPosition.position);
            Player.Instance.PlaySoundOnPlayer(weaponData.fireSounds[Random.Range(0, weaponData.fireSounds.Length)], volume);
        }
    }
}