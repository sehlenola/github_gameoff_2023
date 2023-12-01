using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    private WeaponData weaponDataInstance;
    private float currentCooldown;
    [SerializeField] private int currentWeaponLevel;
    [SerializeField] private int maxWeaponLevel = 100;


    public void SetupWeapon(WeaponData wd)
    {
        weaponData = wd;
        weaponDataInstance = Instantiate(wd);
        currentWeaponLevel = 1;
    }

    public void UpdateWeaponTimer(float timeDelta, float timeMultiplier)
    {
        if(weaponData == null)
        {
            Debug.Log("No weapondata yet");
            return;
        }
        currentCooldown -= timeDelta * timeMultiplier;
        if(currentCooldown < 0)
        {
            weaponData.fireStrategy.Fire(transform, weaponDataInstance);
            currentCooldown = weaponDataInstance.weaponCooldown;
        }
    }
    public WeaponData GetWeaponDataInstance()
    {
        return weaponDataInstance;
    }

    public void UpgradeMe()
    {
        if (currentWeaponLevel >= maxWeaponLevel) { return; }
        currentWeaponLevel++;
        weaponDataInstance.weaponLevel++;
        foreach(WeaponUpgrade u in weaponDataInstance.weaponUpgrades)
        {
            ApplyUpgrade(u);
        }
    }
    public void ApplyUpgrade(WeaponUpgrade upgrade)
    {
        upgrade.ApplyUpgrade(this);
    }
    public int GetCurrentWeaponLevel()
    {
        return weaponDataInstance.weaponLevel;
    }

    void OnDrawGizmos()
    {
        if (weaponDataInstance.areaOfEffect > 0)
        {
            Gizmos.color = Color.red; // Set gizmo color
            Gizmos.DrawWireSphere(transform.position, weaponDataInstance.areaOfEffect * weaponDataInstance.areaMultiplier);
        }
    }

}