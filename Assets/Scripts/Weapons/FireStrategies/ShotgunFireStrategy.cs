using UnityEngine;

public class ShotgunFireStrategy : IProjectileFireStrategy
{
    public int PelletsCount { get; set; }
    public float SpreadAngle { get; set; }

    private ProjectileFactory projectileFactory;

    public ShotgunFireStrategy(ProjectileFactory factory, int initialPelletsCount, float initialSpreadAngle)
    {
        projectileFactory = factory;
        PelletsCount = initialPelletsCount;
        SpreadAngle = initialSpreadAngle;
    }


    public void Fire(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < PelletsCount; i++)
        {
            float angle = SpreadAngle * (i / (float)(PelletsCount - 1) - 0.5f);
            Quaternion spreadRotation = Quaternion.Euler(0, angle, 0);
            //projectileFactory.CreateProjectile(projectilePrefab, position, rotation * spreadRotation);
            ObjectPoolManager.SpawnObject(projectilePrefab, position, rotation * spreadRotation, ObjectPoolManager.PoolType.Gameobject);
            // Additional setup...
        }
    }
}