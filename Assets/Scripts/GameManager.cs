using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private int tripsNeeded;
    [SerializeField] private int currentTrip;
    [SerializeField] private AudioClip winAudio;
    [SerializeField] private AudioClip looseAudio;
    [SerializeField] private AudioClip tripCompleteAudio;
    [SerializeField] private AudioClip portalActivatedAudio;
    private AudioSource audioSource;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject portalGameObject;
    [SerializeField] private GameOverPanel gameOverPanel;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        portalGameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
    }




    private void OnEnable()
    {
        StaticEventHandler.OnGameWon += StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver += StaticEventHandler_OnGameOver;
        StaticEventHandler.OnLevelUp += StaticEventHandler_OnLevelUp;
        StaticEventHandler.OnTripComplete += StaticEventHandler_OnTripComplete;
    }

    private void StaticEventHandler_OnTripComplete(TripCompleteArgs obj)
    {
        currentTrip++;
        if(currentTrip >= tripsNeeded)
        {
            //StaticEventHandler.CallGameOverEvent("Victory!", "All orbs collected!");
            portalGameObject.SetActive(true);
            audioSource.PlayOneShot(portalActivatedAudio);
            uiManager.ShowWarning("Last orb delivered! \n Get to the portal!", 27f);
            StaticEventHandler.CallOnActivatedPortal();

        }
        else
        {
            HandleTripComplete();
        }
    }

    private void OnDisable()
    {
        StaticEventHandler.OnGameWon -= StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver -= StaticEventHandler_OnGameOver;
        StaticEventHandler.OnLevelUp -= StaticEventHandler_OnLevelUp;
        StaticEventHandler.OnTripComplete -= StaticEventHandler_OnTripComplete;
    }

    private void StaticEventHandler_OnGameOver(GameOverArgs obj)
    {
        gameOverPanel.ShowGameOverPanel(obj.titleText, obj.bodyText, FindObjectOfType<DamageTracker>().GetAllWeaponDamageAsString(), false);
        gameOverPanel.gameObject.SetActive(true);
    }

    private void StaticEventHandler_OnGameWon(GameWonArgs obj)
    {
        gameOverPanel.ShowGameOverPanel(obj.titleText, obj.bodyText, FindObjectOfType<DamageTracker>().GetAllWeaponDamageAsString(), true);
        gameOverPanel.gameObject.SetActive(true);
    }
    private void StaticEventHandler_OnLevelUp(LevelUpArgs obj)
    {
        //should level up panel handle all this?
    }

    public int GetCurrentTrip()
    {
        return currentTrip;
    }
    public int GetTripsNeeded()
    {
        return tripsNeeded;
    }


    private void HandleTripComplete()
    {
        audioSource.PlayOneShot(tripCompleteAudio);
        uiManager.ShowWarning("Orb delivered \n Enemy Activity Increased!", 7f);
    }
}
