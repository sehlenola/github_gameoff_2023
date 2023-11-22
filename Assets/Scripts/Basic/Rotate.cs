using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around (up axis by default)
    public float rotationSpeed = 45.0f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate around the specified axis by rotationSpeed degrees per second
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);

    }
}
