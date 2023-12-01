using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{
    private Dictionary<string, float> damageByWeapon = new Dictionary<string, float>();
    private int killCount = 0;

    private void OnEnable()
    {
        StaticEventHandler.OnDamageDealt += StaticEventHandler_HandleDamageDealt;
        StaticEventHandler.OnEnemyKilled += StaticEventHandler_OnEnemyKilled;
    }

    private void StaticEventHandler_OnEnemyKilled(EnemyKilledArgs obj)
    {
        killCount++;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnDamageDealt -= StaticEventHandler_HandleDamageDealt;
        StaticEventHandler.OnEnemyKilled -= StaticEventHandler_OnEnemyKilled;
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
        string result = "Enemies killed: " + killCount + "\n \n";
        foreach (var pair in damageByWeapon)
        {
            string formattedDamage = pair.Value.ToString("N0", System.Globalization.CultureInfo.InvariantCulture).Replace(",", " ");
            result += $"{pair.Key}: {formattedDamage} damage\n";
            PersistentDataManager.Instance.SaveWeaponDamageStatistics(result);
        }
        return result;
    }
    public int GetKillCount()
    {
        return killCount;
    }

}
