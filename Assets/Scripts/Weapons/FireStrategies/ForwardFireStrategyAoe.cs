using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ForwardFireStrategy", menuName = "FireStrategy/Forward Fire AOE")]
public class ForwardFireStrategyAoe : FireStrategy
{
    public float spreadAngle;
    public float fireOffset;
    public override void Fire(Transform firePoint, WeaponData weaponData)
    {
        for (int i = 0; i < weaponData.pelletCount; i++)
        {
            // Use factory to instantiate
            float offsetX = Random.Range(-fireOffset, fireOffset);
            float offsetZ = Random.Range(-fireOffset, fireOffset);
            Vector3 spawnPosition = firePoint.transform.position + new Vector3(offsetX, 0, offsetZ);

            Quaternion spreadRotation = Quaternion.identity;
            if (weaponData.pelletCount > 1)
            {
                float angle = spreadAngle * (i / (float)(weaponData.pelletCount - 1) - 0.5f);
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }

            //projectileFactory.CreateProjectile(projectilePrefab, spawnPosition, rotation * spreadRotation);
            GameObject go = ObjectPoolManager.SpawnObject(weaponData.projectile, spawnPosition, firePoint.transform.rotation * spreadRotation, ObjectPoolManager.PoolType.Gameobject);
            //check if weapon damage has changed since last spawning of a projectile

            ProjectileAoe myProjectile = go.GetComponent<ProjectileAoe>();
            myProjectile.SetWeaponData(weaponData);
            myProjectile.SetDamage(weaponData.weaponDamage);

            PlayFireSound(weaponData, firePoint);

        }
    }
}