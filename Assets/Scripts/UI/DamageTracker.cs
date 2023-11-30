using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{
    private Dictionary<string, float> damageByWeapon = new Dictionary<string, float>();

    private void OnEnable()
    {
        StaticEventHandler.OnDamageDealt += StaticEventHandler_HandleDamageDealt;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnDamageDealt -= StaticEventHandler_HandleDamageDealt;
    }

    private void StaticEventHandler_HandleDamageDealt(DamageEventArgs e)
    {
        if (!damageByWeapon.ContainsKey(e.WeaponData.weaponName))
        {
            damageByWeapon[e.WeaponData.weaponName] = 0;
        }

        damageByWeapon[e.WeaponData.weaponName] += e.DamageAmount;
    }

    public float GetTotalDamageByWeapon(string weaponName)
    {
        if (damageByWeapon.TryGetValue(weaponName, out float totalDamage))
        {
            return totalDamage;
        }

        return 0;
    }

    public string GetAllWeaponDamageAsString()
    {
        string result = "";
        foreach (var pair in damageByWeapon)
        {
            string formattedDamage = pair.Value.ToString("N0", System.Globalization.CultureInfo.InvariantCulture).Replace(",", " ");
            result += $"{pair.Key}: {formattedDamage} damage\n";
        }
        return result;
    }

}
