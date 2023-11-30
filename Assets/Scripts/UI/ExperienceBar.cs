using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private Image experienceBarImage;
    [SerializeField] private TextMeshProUGUI levelText;
    private void OnEnable()
    {
        StaticEventHandler.OnExperiencePickup += StaticEventHandler_OnExperiencePickup;
        StaticEventHandler.OnLevelUp += StaticEventHandler_OnLevelUp;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnExperiencePickup -= StaticEventHandler_OnExperiencePickup;
        StaticEventHandler.OnLevelUp -= StaticEventHandler_OnLevelUp;
    }

    private void StaticEventHandler_OnExperiencePickup(ExperiencePickupArgs obj)
    {
        experienceBarImage.fillAmount = (float)obj.currentExperience / obj.maxExperience;
    }

    private void StaticEventHandler_OnLevelUp(LevelUpArgs obj)
    {
        levelText.text = "Level: " + obj.level.ToString();
    }
}
