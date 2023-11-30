using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileAoe : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float projectileDamage = 1f;
    [SerializeField] private GameObject floatingDamagePrefab;
    [SerializeField] GameObject aoeEffectPrefab;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private LayerMask enemyLayer;

    private Coroutine _returnToPoolTimerCoroutine;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
        speed = Player.Instance.GetPlayerSpeed();
        transform.rotation = Player.Instance.transform.rotation;
        trailRenderer.emitting = true;
    }
    private void OnDisable()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ExplodeDamage(weaponData);
        ExplodeVisuals();
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    public float GetProjectileDamage()
    {
        return projectileDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Assuming the enemy has a component that implements ITakeDamage
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                //damageable.TakeDamage(projectileDamage);
                //SpawnDamageNumber(other.transform.position);
                ExplodeVisuals();
                ExplodeDamage(weaponData);
                //StaticEventHandler.CallOnDamageDealt(weaponData, projectileDamage);

            }

            // Destroy or deactivate the projectile
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    void SpawnDamageNumber(Vector3 enemyPosition)
    {
        // Instantiate the damage number prefab at a random position near the enemy

        GameObject go = ObjectPoolManager.SpawnObject(floatingDamagePrefab, enemyPosition + new Vector3(0, 1), Quaternion.Euler(90, 0, 0), ObjectPoolManager.PoolType.ParticleSystem);
        //check if weapon damage has changed since last spawning of a projectile

        go.GetComponent<DamageNumber>().SetDamage(projectileDamage);
    }

    public void SetWeaponData(WeaponData wd)
    {
        weaponData = wd;
    }

    private void ExplodeVisuals()
    {
        // Instantiate and scale AOE visual effect
        GameObject aoeEffect = ObjectPoolManager.SpawnObject(aoeEffectPrefab, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
        aoeEffect.transform.localScale = new Vector3(weaponData.areaOfEffect * weaponData.areaMultiplier, aoeEffect.transform.localScale.y, weaponData.areaOfEffect * weaponData.areaMultiplier);
    }


    public void ExplodeDamage(WeaponData weaponData)
    {
        // Perform AOE damage logic
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
}
