using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : SingletonMonobehaviour<Player>, ITakeDamage
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxExperience = 10;
    [SerializeField] private int currentExperience;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject hpObject;


    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHpBar();
    }

    private void Update()
    {
        hpObject.transform.position = transform.position + new Vector3(0, 0, -2);
        hpObject.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
    }
    public void TakeDamage(float amount)
    {
        // Player damage logic
        currentHealth -= (int)amount;
        UpdateHpBar();
        if (currentHealth <= 0)
        {
            StaticEventHandler.CallGameOverEvent("Game Over!", "Player died");
            Destroy(gameObject);
        }
    }
    public void GainExperience(int experience)
    {
        currentExperience += experience;
        if(currentExperience > maxExperience)
        {
            LevelUp();
            currentExperience = 0;
        }
    }

    private void LevelUp()
    {
        StaticEventHandler.CallOnLevelUpEvent(1);
    }
    private void UpdateHpBar()
    {
        hpBar.fillAmount = currentHealth / (float)maxHealth;
    }


}

