using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : SingletonMonobehaviour<Orb>, ITakeDamage
{
    public Transform startPoint;
    public Transform endPoint;

    private Transform currentTarget;
    private bool isMovingToEndPoint;
    private float speed;
    private float retreatSpeed; 
    [SerializeField] private float retreatSpeedMultiplier = 4;
    [SerializeField] private GameObject orbVisual;

    public float playTime = 60f; // Duration from start to end
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    [SerializeField] float orbCarrierRadius = 1f;
    [SerializeField] private GameObject orbCarrierPrefab;
    [SerializeField] private AudioClip orbCarrierDeathClip;
    private List<GameObject> orbCarriers = new List<GameObject>();
    private AudioSource audioSource;

    private float startTime;


    private void OnEnable()
    {
        StaticEventHandler.OnGameWon += StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver += StaticEventHandler_OnGameOver;
        StaticEventHandler.OnTripComplete += StaticEventHandler_OnTripComplete;
        StaticEventHandler.OnPortalActivated += StaticEventHandler_OnPortalActivated;
    }

    private void StaticEventHandler_OnPortalActivated(PortalActivatedArgs obj)
    {
        speed = 0f; retreatSpeed = 0;
    }

    private void StaticEventHandler_OnTripComplete(TripCompleteArgs obj)
    {

    }

    private void StaticEventHandler_OnGameOver(GameOverArgs obj)
    {
        speed = 0f; retreatSpeed = 0;
        Destroy(gameObject);
    }

    private void StaticEventHandler_OnGameWon(GameWonArgs obj)
    {
        speed = 0f; retreatSpeed = 0;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        StaticEventHandler.OnGameWon -= StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver -= StaticEventHandler_OnGameOver;
        StaticEventHandler.OnTripComplete -= StaticEventHandler_OnTripComplete;
        StaticEventHandler.OnPortalActivated -= StaticEventHandler_OnPortalActivated;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        SpawnOrbCarriers();
        startTime = Time.time;
        ResetOrb();
    }

    private void SpawnOrbCarriers()
    {
        for (int i = 0; i < currentHealth; i++)
        {
            float angle = i * Mathf.PI * 2f / currentHealth;
            Vector3 position = new Vector3(Mathf.Cos(angle) * orbCarrierRadius, 0, Mathf.Sin(angle) * orbCarrierRadius);
            GameObject healthIndicator = Instantiate(orbCarrierPrefab, transform.position + position, Quaternion.identity, transform);
            orbCarriers.Add(healthIndicator);
        }
    }

    void Update()
    {
        if (currentTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        if(isMovingToEndPoint) transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        if(!isMovingToEndPoint) transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, retreatSpeed * Time.deltaTime);

        if (transform.position == currentTarget.position)
        {
            if (isMovingToEndPoint)
            {
                //reached endpoint
                orbVisual.SetActive(false);
                StaticEventHandler.CallOnTripCompleteEvent(1);
                currentTarget = startPoint;
                isMovingToEndPoint = false;
            }
            else
            {
                //reached startpoint
                orbVisual.SetActive(true);
                StaticEventHandler.CallOnTripStartedEvent(1);
                currentTarget = endPoint;
                isMovingToEndPoint = true;
            }

            //CalculateSpeed(); // Recalculate speed in case the distance changes
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // Damage logic here
        for (int i = 0; i < (int)amount; i++)
        {
            if (orbCarriers.Count > 0)
            {
                audioSource.Play();
                GameObject indicatorToDestroy = orbCarriers[orbCarriers.Count - 1];
                orbCarriers.RemoveAt(orbCarriers.Count - 1);
                Destroy(indicatorToDestroy);
            }
        }



        if (currentHealth <= 0)
        {
            StaticEventHandler.CallGameOverEvent("Game Over", "The light orb was destroyed!");
            Destroy(gameObject);
            
        }
    }

    private IEnumerator EndGameCountdown()
    {
        yield return new WaitForSeconds(10);
        // Implement the logic for winning the game
    }


    void ResetOrb()
    {
        transform.position = startPoint.position;
        currentTarget = endPoint;
        isMovingToEndPoint = true;
        CalculateSpeed();
        orbVisual.SetActive(true);
        StaticEventHandler.CallOnTripStartedEvent(1);
    }

    void CalculateSpeed()
    {
        float distance = Vector3.Distance(startPoint.position, endPoint.position);
        speed = distance / playTime;
        retreatSpeed = speed * retreatSpeedMultiplier;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Assuming the enemy has a component that implements ITakeDamage
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(5000);
                TakeDamage(1);

            }
        }
    }
}
