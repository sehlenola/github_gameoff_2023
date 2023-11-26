using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{

    [SerializeField] private Transform targetTransform;
    [SerializeField] private Coroutine blinkCoroutine;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float blinkDuration = 0.4f;
    [SerializeField] private float blinkFrequency = 0.1f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    private bool isFlashing = false;
    [SerializeField] private GameObject experiencePickupPrefab;
    [SerializeField] private GameObject visualLeft;
    [SerializeField] private GameObject visualRight;

    [SerializeField] private Material defaultMaterial; // Assign the default material
    [SerializeField] private Material flashMaterial;   // Assign a material to indicate damage
    private Renderer enemyRenderer;



    private void StaticEventHandler_OnGameOver(GameOverArgs obj)
    {
        targetTransform = null;
    }

    private void StaticEventHandler_OnGameWon(GameWonArgs obj)
    {
        targetTransform = null;
    }

    private void Awake()
    {
        enemyRenderer = GetComponentInChildren<Renderer>();
        enemyRenderer.material = defaultMaterial;
    }
    private void Start()
    {
        //targetTransform = Player.Instance.transform;
        targetTransform = Orb.Instance.transform;
        StaticEventHandler.OnGameWon += StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver += StaticEventHandler_OnGameOver;
    }

    private void OnDestroy()
    {
        StaticEventHandler.OnGameWon -= StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver -= StaticEventHandler_OnGameOver;
    }
    private void OnEnable()
    {
        currentHealth = maxHealth;
        targetTransform = Orb.Instance.transform;
        if (targetTransform != null)
        {
            if(targetTransform.transform.position.x > transform.position.x)
            {
                SetVisual(true);
            }
            else
            {
                SetVisual(false);
            }
        }
    }

    private void SetVisual(bool v)
    {
        visualLeft.SetActive(!v);
        visualRight.SetActive(v);
    }

    private void OnDisable()
    {
        enemyRenderer.material = defaultMaterial;
        isFlashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        }
    }




    public void TakeDamage(float amount)
    {
        // Enemy damage logic
        currentHealth -= Mathf.RoundToInt(amount);
        if(currentHealth <= 0)
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
            }
            DropExperience();
            StaticEventHandler.CallOnEnemyKilledEvent();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            return;
        }

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(FlashEffect());
    }

    private void DropExperience()
    {
        GameObject go = ObjectPoolManager.SpawnObject(experiencePickupPrefab, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

    }

    IEnumerator FlashEffect()
    {
        float duration = 0.5f;
        float interval = 0.3f;
        float elapsed = 0;

        isFlashing = true;

        while (elapsed < duration)
        {
            // Toggle the material based on the flashing state
            enemyRenderer.material = isFlashing ? flashMaterial : defaultMaterial;

            // Flip the state
            isFlashing = !isFlashing;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // Ensure the material is reset to default after flashing
        enemyRenderer.material = defaultMaterial;
        isFlashing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the enemy has a component that implements ITakeDamage
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(1f);


            }

            StaticEventHandler.CallOnEnemyKilledEvent();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}