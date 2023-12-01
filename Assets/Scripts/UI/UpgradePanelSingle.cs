using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelSingle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI weaponDescriptionText;
    [SerializeField] TextMeshProUGUI newOrUpgradeText;
    [SerializeField] Image weaponSprite;

    [SerializeField] WeaponData weaponData;

    [SerializeField] LevelUpPanel levelUpPanel;
    [SerializeField] UpgradeSpriteTextCombo weaponSpriteComboTemplate;
    [SerializeField] GameObject weaponSpriteComboHolder;

    [SerializeField] Button upgradePanelSingleButton;
    private SlideInAnimation slideInAnimation;

    private void Awake()
    {
        slideInAnimation = GetComponent<SlideInAnimation>();
        upgradePanelSingleButton = GetComponent<Button>();
        levelUpPanel = FindObjectOfType<LevelUpPanel>();
    }


    void Start()
    {

        // Add click listener
        upgradePanelSingleButton.onClick.AddListener(() => ButtonClicked());
    }

    void ButtonClicked()
    {
        levelUpPanel.UpgradeWeaponData(weaponData);
        SelectUpgradePanel();
    }


    public void SetupUpgradePanel(WeaponData wd, bool isUpgrade, int weaponLevel)
    {
        weaponData = wd;
        newOrUpgradeText.text = "NEW";
        if (isUpgrade)
        {
            newOrUpgradeText.text = "Level: " + weaponLevel;
            weaponNameText.text = weaponData.weaponName;
            string weaponUpgradeText = "Upgrades: \n";
            foreach (WeaponUpgrade upgrade in weaponData.weaponUpgrades)
            {
                weaponUpgradeText = weaponUpgradeText + upgrade.upgradeName + "\n";
                UpgradeSpriteTextCombo newSpriteTextCombo = Instantiate(weaponSpriteComboTemplate, weaponSpriteComboHolder.transform);
                newSpriteTextCombo.SetupSpriteTextCombo(upgrade.upgradeSprite, upgrade.upgradeName);
            }
            weaponDescriptionText.text = weaponUpgradeText;
            weaponDescriptionText.text = weaponData.weaponDescription;
        }
        else
        {
            weaponNameText.text = weaponData.weaponName;
            weaponDescriptionText.text = weaponData.weaponDescription;
            foreach (WeaponUpgrade upgrade in weaponData.weaponUpgrades)
            {
                UpgradeSpriteTextCombo newSpriteTextCombo = Instantiate(weaponSpriteComboTemplate, weaponSpriteComboHolder.transform);
                newSpriteTextCombo.SetupSpriteTextCombo(upgrade.upgradeSprite, upgrade.upgradeName);
            }
        }
        weaponSprite.sprite = weaponData.weaponSprite;

    }
    public void SelectUpgradePanel()
    {
        WeaponManager wm = FindObjectOfType<WeaponManager>();
        bool isUpgrade = wm.AlreadyHaveWeapon(weaponData);
        if (isUpgrade)
        {
            wm.UpgradeWeapon(weaponData);
        }
        else
        {
            wm.AddWeapon(weaponData);
        }
    }

}

