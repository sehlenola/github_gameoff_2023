using System.Collections;
using UnityEngine;

public class Orb : MonoBehaviour, ITakeDamage
{
    public Transform startPoint;
    public Transform endPoint;
    public float playTime = 60f; // Duration from start to end
    public float health = 100f;

    private float startTime;

    void Start()
    {
        transform.position = startPoint.position;
        startTime = Time.time;
    }

    void Update()
    {
        float journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        float distCovered = (Time.time - startTime) * (journeyLength / playTime);
        float journeyFraction = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, journeyFraction);

        if (transform.position == endPoint.position)
        {

            StartCoroutine(EndGameCountdown());
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
            // Handle orb destruction, like losing the game
        }
    }

    private IEnumerator EndGameCountdown()
    {
        yield return new WaitForSeconds(10);
        // Implement the logic for winning the game
    }
}
