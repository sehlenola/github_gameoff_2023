using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    private WeaponData weaponDataInstance;
    private float currentCooldown;


    public void SetupWeapon(WeaponData wd)
    {
        weaponData = wd;
        weaponDataInstance = Instantiate(wd);
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
}