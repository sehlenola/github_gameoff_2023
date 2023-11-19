using UnityEngine;

[CreateAssetMenu(fileName = "AOEFireStrategy", menuName = "FireStrategy/AOE Blast")]
public class AreaFireStrategySO : FireStrategy
{
    public GameObject aoeEffectPrefab;

    public override void Fire(Transform firePoint, WeaponData weaponData)
    {
        // Perform AOE damage logic
        Collider[] hitColliders = Physics.OverlapSphere(firePoint.position, weaponData.areaOfEffect * weaponData.areaMultiplier);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // Assuming your enemies have the tag "Enemy"
            {
                // Apply damage to enemies
            }
        }

        // Instantiate and scale AOE visual effect
        GameObject aoeEffect = ObjectPoolManager.SpawnObject(aoeEffectPrefab, firePoint.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
        aoeEffect.transform.localScale = new Vector3(weaponData.areaOfEffect * weaponData.areaMultiplier, aoeEffect.transform.localScale.y, weaponData.areaOfEffect * weaponData.areaMultiplier);

        // Destroy the effect after some time
        //Destroy(aoeEffect, 1.0f); // Adjust time as needed
    }
}