using UnityEngine;

public class MachineGunFireStrategy : IProjectileFireStrategy
{
    // These properties aren't really used for a machine gun, but we include them to satisfy the interface.
    public int PelletsCount { get; set; } = 1; // Only one bullet per shot
    public float SpreadAngle { get; set; } = 5; // No spread angle
    private ProjectileFactory projectileFactory;

    public MachineGunFireStrategy(ProjectileFactory factory)
    {
        projectileFactory = factory;
    }


    public void Fire(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < PelletsCount; i++) {
            // Use factory to instantiate
            float offsetX = Random.Range(-0.5f, 0.5f);
            float offsetZ = Random.Range(-0.5f, 0.5f);
            Vector3 spawnPosition = position + new Vector3(offsetX, 0, offsetZ);

            Quaternion spreadRotation = Quaternion.identity;
            if (PelletsCount > 1)
            {
                float angle = SpreadAngle * (i / (float)(PelletsCount - 1) - 0.5f);
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }

            //projectileFactory.CreateProjectile(projectilePrefab, spawnPosition, rotation * spreadRotation);
            ObjectPoolManager.SpawnObject(projectilePrefab, spawnPosition, rotation * spreadRotation, ObjectPoolManager.PoolType.Gameobject);
            
        }
    }
}