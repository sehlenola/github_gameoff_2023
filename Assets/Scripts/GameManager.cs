using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private int tripsNeeded;
    [SerializeField] private int currentTrip;


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
            StaticEventHandler.CallGameOverEvent("Victory!", "All orbs collected!");
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
        Debug.Log(obj.titleText);
        Debug.Log(obj.bodyText);
    }

    private void StaticEventHandler_OnGameWon(GameWonArgs obj)
    {
        Debug.Log(obj.titleText);
        Debug.Log(obj.bodyText);
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
}
