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

    private void Awake()
    {
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


    public void SetupUpgradePanel(WeaponData wd, bool isUpgrade)
    {
        weaponData = wd;
        newOrUpgradeText.text = "NEW";
        if (isUpgrade)
        {
            newOrUpgradeText.text = "UPGRADE!";

            string weaponUpgradeText = "Upgrades: \n";
            foreach (WeaponUpgrade upgrade in weaponData.weaponUpgrades)
            {
                weaponUpgradeText = weaponUpgradeText + upgrade.upgradeName + "\n";
                UpgradeSpriteTextCombo newSpriteTextCombo = Instantiate(weaponSpriteComboTemplate, weaponSpriteComboHolder.transform);
                newSpriteTextCombo.SetupSpriteTextCombo(upgrade.upgradeSprite, upgrade.upgradeName);
            }
            weaponDescriptionText.text = weaponUpgradeText;
            weaponDescriptionText.text = "";
        }
        else
        {
            weaponNameText.text = weaponData.weaponName;
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