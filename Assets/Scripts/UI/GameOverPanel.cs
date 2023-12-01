using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{

    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statText;


    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowGameOverPanel(string title, string description, string stat, bool won)
    {
        titleText.text = title;
        descriptionText.text = description;
        statText.text = stat;
        if (won)
        {
            retryButton.SetActive(false);
            continueButton.SetActive(true);
        }
        else
        {
            retryButton.SetActive(true);
            continueButton.SetActive(false);
        }

    }


}
