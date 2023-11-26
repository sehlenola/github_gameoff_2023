using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    [SerializeField] private int experienceValue = 1;
    [SerializeField] private bool pickedUp;
    [SerializeField] private float lifeTime = 30f;
    private Coroutine _returnToPoolTimerCoroutine;

    private void OnEnable()
    {
        pickedUp = false;
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
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

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }


}
