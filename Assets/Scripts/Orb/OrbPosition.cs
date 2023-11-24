using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPosition : MonoBehaviour
{
    [SerializeField] private GameObject visualOrbPrefab;
    [SerializeField] private float randomOffset = 3f;
    [SerializeField] private bool startPoint;
    private Queue<GameObject> visualOrbs = new Queue<GameObject>();

    private void Start()
    {
        if (startPoint)
        {
            SetupOrbs(GameManager.Instance.GetTripsNeeded());
        }
    }

    private void OnEnable()
    {
        StaticEventHandler.OnTripStarted += StaticEventHandler_OnTripStarted;
        StaticEventHandler.OnTripComplete += StaticEventHandler_OnTripComplete;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnTripStarted -= StaticEventHandler_OnTripStarted;
        StaticEventHandler.OnTripComplete -= StaticEventHandler_OnTripComplete;
    }
    private void StaticEventHandler_OnTripComplete(TripCompleteArgs obj)
    {
        if (!startPoint)
        {
            SpawnOrb();
        }
    }

    private void StaticEventHandler_OnTripStarted(TripStartArgs obj)
    {
        if (startPoint)
        {
            TakeOrb();
        }
    }

    public void SetupOrbs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject orb = SpawnOrb();
            visualOrbs.Enqueue(orb);
        }
    }

    public void TakeOrb()
    {
        if (visualOrbs.Count > 0)
        {
            Destroy(visualOrbs.Dequeue());
        }
    }


    public GameObject SpawnOrb()
    {
        return Instantiate(visualOrbPrefab, transform.position + new Vector3(Random.Range(-randomOffset, randomOffset),0, Random.Range(-randomOffset, randomOffset)), Quaternion.identity);
    }
}
