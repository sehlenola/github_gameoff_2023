using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/New Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public FireStrategy fireStrategy;
    public float weaponCooldown;
    public float weaponDamage;
    public int pelletCount;
    public GameObject projectile;
    public Sprite weaponSprite;
    public AudioClip[] fireSounds;
    // Add more properties as needed (e.g., damage, fire rate, etc.)
}