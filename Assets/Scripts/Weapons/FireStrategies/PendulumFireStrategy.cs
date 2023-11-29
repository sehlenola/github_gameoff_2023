using UnityEngine;

[CreateAssetMenu(fileName = "PendulumFireStrategy", menuName = "FireStrategy/Pendulum Fire")]
public class PendulumFireStrategy : FireStrategy
{
    public float angleOffset = 180f; // Offset angle in degrees
    public float arcAngle = 90f; // Total arc angle in degrees
    public float swingSpeed = 15f; // Speed of the pendulum swing
    private float currentAngle = 0f; // Current angle of the pendulum

    public override void Fire(Transform firePoint, WeaponData weaponData)
    {

        float dotProduct = Vector3.Dot(firePoint.forward, Vector3.right);
        bool isFlipped = dotProduct < 0 ? true : false;
        currentAngle = Mathf.Sin(Time.time * swingSpeed) * arcAngle + angleOffset;


        currentAngle = isFlipped ? -currentAngle : currentAngle;
        Quaternion pendulumRotation = Quaternion.Euler(0, currentAngle, 0);
        Quaternion combinedRotation = firePoint.rotation * pendulumRotation;

        GameObject go = ObjectPoolManager.SpawnObject(weaponData.projectile, firePoint.position, combinedRotation, ObjectPoolManager.PoolType.Gameobject);
        Projectile myProjectile = go.GetComponent<Projectile>();
        myProjectile.SetDamage(weaponData.weaponDamage);
        myProjectile.SetWeaponData(weaponData);
        PlayFireSound(weaponData, firePoint);

        PlayFireSound(weaponData, firePoint);
    }
}