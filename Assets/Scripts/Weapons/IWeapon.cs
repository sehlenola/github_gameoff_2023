public interface IWeapon
{
    WeaponData WeaponData { get; set; }
    WeaponData WeaponDataInstance { get; set; }
    float CooldownTime { get; }
    float BaseDamage { get; set; }
    void Fire();
    void Upgrade();

    void UpdateWeaponCooldown(float timeDelta, float cooldownMultiplier);
}