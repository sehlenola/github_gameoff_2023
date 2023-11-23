using System;
using UnityEngine;

public class Player : SingletonMonobehaviour<Player>, ITakeDamage
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxExperience = 10;
    [SerializeField] private int currentExperience;


    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        // Player damage logic
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


}

