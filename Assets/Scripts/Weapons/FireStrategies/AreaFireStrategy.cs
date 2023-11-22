using UnityEngine;

[CreateAssetMenu(fileName = "AOEFireStrategy", menuName = "FireStrategy/AOE Blast")]
public class AreaFireStrategySO : FireStrategy
{
    public GameObject aoeEffectPrefab;
    [SerializeField] float areaOfEffectDamage;
    [SerializeField] GameObject floatingDamagePrefab;
    [SerializeField] private LayerMask enemyLayer;

    public override void Fire(Transform firePoint, WeaponData weaponData)
    {
        // Perform AOE damage logic
        SetDamage(weaponData.weaponDamage);
        Collider[] hitColliders = Physics.OverlapSphere(firePoint.position, weaponData.areaOfEffect * weaponData.areaMultiplier, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // Assuming your enemies have the tag "Enemy"
            {
                ITakeDamage damageable = hitCollider.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    damageable.TakeDamage(weaponData.weaponDamage);
                    SpawnDamageNumber(hitCollider.transform.position);

                }
            }
        }

        // Instantiate and scale AOE visual effect
        GameObject aoeEffect = ObjectPoolManager.SpawnObject(aoeEffectPrefab, firePoint.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
        aoeEffect.transform.localScale = new Vector3(weaponData.areaOfEffect * weaponData.areaMultiplier, aoeEffect.transform.localScale.y, weaponData.areaOfEffect * weaponData.areaMultiplier);
    }

    void SpawnDamageNumber(Vector3 enemyPosition)
    {
        // Instantiate the damage number prefab at a random position near the enemy

        GameObject go = ObjectPoolManager.SpawnObject(floatingDamagePrefab, enemyPosition + new Vector3(0, 1), Quaternion.Euler(90, 0, 0), ObjectPoolManager.PoolType.ParticleSystem);
        //check if weapon damage has changed since last spawning of a projectile

        go.GetComponent<DamageNumber>().SetDamage(areaOfEffectDamage);
    }

    public void SetDamage(float damage)
    {
        areaOfEffectDamage = damage;
    }
}