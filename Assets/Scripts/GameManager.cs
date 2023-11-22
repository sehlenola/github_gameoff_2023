using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        StaticEventHandler.OnGameWon += StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver += StaticEventHandler_OnGameOver;
        StaticEventHandler.OnLevelUp += StaticEventHandler_OnLevelUp;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnGameWon -= StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver -= StaticEventHandler_OnGameOver;
        StaticEventHandler.OnLevelUp -= StaticEventHandler_OnLevelUp;
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
}
