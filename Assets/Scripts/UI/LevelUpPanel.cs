using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] GameObject upgradePanelHolder;
    [SerializeField] GameObject upgradePanelPrefab;

    [SerializeField] WeaponData[] allWeaponData;

    private void OnEnable()
    {
        
    }


    private void OnDisable()
    {
        //StaticEventHandler.OnLevelUp -= StaticEventHandler_OnLevelUp;
    }

    private void OnDestroy()
    {
        StaticEventHandler.OnLevelUp -= StaticEventHandler_OnLevelUp;
    }


    private void Start()
    {
        //ShowUpgradePanels(1);
        UpgradeWeaponData(null);
        StaticEventHandler.OnLevelUp += StaticEventHandler_OnLevelUp;
        gameObject.SetActive(false);
    }



    private void StaticEventHandler_OnLevelUp(LevelUpArgs obj)
    {
        ShowUpgradePanels(3);
    }

    public void ShowUpgradePanels(int countToShow)
    {
        ClearUpgradePanels();
        upgradePanelHolder.GetComponent<SlideInAnimation>().AnimateIn();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        //spawn upgradePanels
        List<WeaponData> chosenWeapons = GetRandomUniqueWeapons(countToShow);
        foreach (WeaponData weaponData in chosenWeapons)
        {
            UpgradePanelSingle upgradePanelSingle = Instantiate(upgradePanelPrefab, upgradePanelHolder.transform).GetComponent<UpgradePanelSingle>();
            WeaponManager wm = FindObjectOfType<WeaponManager>();
            bool isUpgrade = wm.AlreadyHaveWeapon(weaponData);
            int weaponLevel = wm.GetWeaponLevel(weaponData);
            upgradePanelSingle.SetupUpgradePanel(weaponData, isUpgrade, weaponLevel);
            //upgradePanelSingle.ShowPanel();
        }
    }

    private WeaponData[] GetRandomWeapons(int count)
    {
        return null;
    }

    public void UpgradeWeaponData(WeaponData wd)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ClearUpgradePanels()
    {
        foreach (Transform child in upgradePanelHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public List<WeaponData> GetRandomUniqueWeapons(int numberOfWeapons)
    {
        List<WeaponData> randomWeapons = new List<WeaponData>();
        List<WeaponData> availableWeapons = new List<WeaponData>(allWeaponData); // Copy to avoid modifying the original list

        while (randomWeapons.Count < numberOfWeapons && availableWeapons.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableWeapons.Count);
            randomWeapons.Add(availableWeapons[randomIndex]);
            availableWeapons.RemoveAt(randomIndex); // Remove to avoid duplicates
        }

        return randomWeapons;
    }


}
