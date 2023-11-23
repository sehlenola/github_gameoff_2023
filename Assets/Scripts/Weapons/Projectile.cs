using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float projectileDamage = 1f;
    [SerializeField] private GameObject floatingDamagePrefab;

    private Coroutine _returnToPoolTimerCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
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
        while(elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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
                damageable.TakeDamage(projectileDamage);
                SpawnDamageNumber(other.transform.position);

            }

            // Destroy or deactivate the projectile
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    void SpawnDamageNumber(Vector3 enemyPosition)
    {
        // Instantiate the damage number prefab at a random position near the enemy

        GameObject go = ObjectPoolManager.SpawnObject(floatingDamagePrefab, enemyPosition + new Vector3(0,1), Quaternion.Euler(90, 0, 0), ObjectPoolManager.PoolType.ParticleSystem);
        //check if weapon damage has changed since last spawning of a projectile

        go.GetComponent<DamageNumber>().SetDamage(projectileDamage);
    }
}
