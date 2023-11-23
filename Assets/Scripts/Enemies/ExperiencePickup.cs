using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    [SerializeField] private int experienceValue = 1;
    [SerializeField] private bool pickedUp;

    private void OnEnable()
    {
        pickedUp = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp)
        {
            pickedUp = true;
            Player.Instance.GainExperience(experienceValue);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }

    }
}
