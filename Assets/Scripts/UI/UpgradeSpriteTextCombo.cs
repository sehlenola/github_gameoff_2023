using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeSpriteTextCombo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] Image upgradeImage;

    public void SetupSpriteTextCombo(Sprite img, string txt)
    {
        upgradeImage.sprite = img;
        upgradeText.text = txt;
    }
}
