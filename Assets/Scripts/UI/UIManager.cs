using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningText;
    private void Start()
    {
        warningText.enabled = false;
    }
    public void ShowWarning(string message, float duration)
    {
        warningText.text = message;
        StartCoroutine(FlashText(duration));
    }

    private IEnumerator FlashText(float duration)
    {
        float endTime = Time.time + duration;
        bool isVisible = true;

        while (Time.time < endTime)
        {
            // Toggle visibility
            warningText.enabled = isVisible;
            isVisible = !isVisible;

            yield return new WaitForSeconds(0.5f);
        }

        warningText.enabled = false;
    }
}