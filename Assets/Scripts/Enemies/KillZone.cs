using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(100f);


            }
        }
    }
}
