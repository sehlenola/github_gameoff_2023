using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFalling : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private Vector3 gravityDirection = new Vector3(0,0,-1);
    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        velocity = Vector3.zero;
    }

    private void Update()
    {
        Vector3 gravityAcceleration = gravityDirection.normalized * gravity * Time.deltaTime;
        velocity += gravityAcceleration;
        transform.position += velocity * Time.deltaTime;
    }
}
