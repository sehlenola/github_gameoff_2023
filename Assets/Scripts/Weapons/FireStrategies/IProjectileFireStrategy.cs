using UnityEngine;

public interface IProjectileFireStrategy
{
    int PelletsCount { get; set; }
    float SpreadAngle { get; set; }
    void Fire(GameObject projectilePrefab, Vector3 position, Quaternion rotation);
}