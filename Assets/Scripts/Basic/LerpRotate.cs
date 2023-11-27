using UnityEngine;

public class LerpRotate : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 1f; // Speed of interpolation
    [SerializeField] private Vector3 target; // Speed of interpolation

    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
    }
    private void OnDisable()
    {
        //transform.rotation = Quaternion.identity;
    }
}