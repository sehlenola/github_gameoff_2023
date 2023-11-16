using UnityEngine;

public class ProjectileFactory
{
    public GameObject CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(prefab, position, rotation);
    }
}
