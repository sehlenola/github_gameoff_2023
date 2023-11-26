using UnityEngine;

public class AircraftVisualUp : MonoBehaviour
{
    [SerializeField] private Transform visualAircraft; // Assign the visual GameObject of the aircraft
    [SerializeField] private Transform player; // Assign the visual GameObject of the player
    [SerializeField] private float rotationSpeed = 2f; // Speed at which the aircraft rotates

    void Update()
    {
        // Your existing movement logic here

        RotateAircraft();
    }

    void RotateAircraft()
    {
        float dotProduct = Vector3.Dot(player.forward, Vector3.right);
        float targetRotationZ = dotProduct < 0 ? -90f : 90f; // 180 degrees if upside down, otherwise 0

        // Smoothly rotate the visual GameObject of the aircraft
        float zRotation = Mathf.LerpAngle(visualAircraft.eulerAngles.z, targetRotationZ, Time.deltaTime * rotationSpeed);

        visualAircraft.localRotation = Quaternion.Euler(player.rotation.x, player.rotation.y, zRotation);
    }
}