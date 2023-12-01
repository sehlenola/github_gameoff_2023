using UnityEngine;
public class Explosion : MonoBehaviour
{

    [SerializeField] float areaOfEffectDamage;
    [SerializeField] GameObject floatingDamagePrefab;
    [SerializeField] private LayerMask enemyLayer;

    public void Explode(WeaponData weaponData)
    {
        // Perform AOE damage logic
        Player.Instance.PlaySoundOnPlayer(weaponData.fireSounds[Random.Range(0, weaponData.fireSounds.Length)]);
        SetDamage(weaponData.weaponDamage);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, weaponData.areaOfEffect * weaponData.areaMultiplier, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // Assuming your enemies have the tag "Enemy"
            {
                ITakeDamage damageable = hitCollider.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    damageable.TakeDamage(weaponData.weaponDamage);
                    SpawnDamageNumber(hitCollider.transform.position);
                    StaticEventHandler.CallOnDamageDealt(weaponData, weaponData.weaponDamage);

                }
            }
        }
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