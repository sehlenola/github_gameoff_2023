using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : SingletonMonobehaviour<Player>, ITakeDamage
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxExperience = 5;
    [SerializeField] private int currentExperience;
    [SerializeField] private int currentLevel;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject hpObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip healAudio;
    [SerializeField] private PlayerController playerController;



    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentLevel = 1;
        audioSource = GetComponent<AudioSource>();
        StaticEventHandler.OnTripComplete += StaticEventHandler_OnTripComplete;
        StaticEventHandler.CallOnLevelUpEvent(currentLevel);
        currentHealth = maxHealth;
        UpdateHpBar();
        StaticEventHandler.CallOnExperiencePickupEvent(currentExperience, maxExperience);
    }
    private void OnDisable()
    {
        StaticEventHandler.OnTripComplete -= StaticEventHandler_OnTripComplete;
    }

    private void StaticEventHandler_OnTripComplete(TripCompleteArgs obj)
    {
        HealDamage(1);
        audioSource.PlayOneShot(healAudio);
    }

    private void Update()
    {
        hpObject.transform.position = transform.position + new Vector3(0, 0, -2);
        hpObject.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
    }

    private void HealDamage(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        UpdateHpBar();
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
        StaticEventHandler.CallOnExperiencePickupEvent(currentExperience, maxExperience);
    }

    private void LevelUp()
    {
        maxExperience+= currentLevel * 2;
        currentLevel++;
        StaticEventHandler.CallOnLevelUpEvent(currentLevel);
    }
    private void UpdateHpBar()
    {
        hpBar.fillAmount = currentHealth / (float)maxHealth;
    }
    public float GetPlayerSpeed()
    {
        return playerController.GetSpeed();
    }


}

