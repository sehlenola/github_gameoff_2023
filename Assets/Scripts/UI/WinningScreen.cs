using TMPro;
using UnityEngine;

public class WinningScreen : MonoBehaviour
{
    public TextMeshProUGUI weaponDamageText;

    private void OnEnable()
    {
        weaponDamageText.text = PersistentDataManager.Instance.WeaponDamageStatistics;
    }
}