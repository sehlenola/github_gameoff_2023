using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] GameObject upgradePanelHolder;
    [SerializeField] GameObject upgradePanelPrefab;

    [SerializeField] WeaponData[] weaponDatas;

    private void Start()
    {
        ShowUpgradePanels(1);
    }
    public void ShowUpgradePanels(int countToShow)
    {
        //spawn upgradePanels
        foreach(WeaponData weaponData in weaponDatas)
        {
            UpgradePanelSingle upgradePanelSingle = Instantiate(upgradePanelPrefab, upgradePanelHolder.transform).GetComponent<UpgradePanelSingle>();
            WeaponManager wm = FindObjectOfType<WeaponManager>();
            bool isUpgrade = wm.AlreadyHaveWeapon(weaponData);
            upgradePanelSingle.SetupUpgradePanel(weaponData, isUpgrade);
        }
    }

    private WeaponData[] GetRandomWeapons(int count)
    {
        return null;
    }

    public void UpgradeWeaponData(WeaponData wd)
    {
        gameObject.SetActive(false);
    }
}
