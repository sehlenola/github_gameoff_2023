public interface IWeapon
{
    WeaponData WeaponData { get; set; }
    float BaseDamage { get; set; }
    void Fire();
    void Upgrade();
}