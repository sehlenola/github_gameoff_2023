public interface IWeapon
{
    float BaseDamage { get; set; }
    void Fire();
    void Upgrade();
}