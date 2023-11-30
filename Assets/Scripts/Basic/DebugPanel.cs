using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugPanel : MonoBehaviour
{

    public void LevelUp()
    {
        StaticEventHandler.CallOnLevelUpEvent(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        StaticEventHandler.CallGameOverEvent("LOST!", "DEBUG LOST! \n");
    }
    public void GameWon()
    {
        StaticEventHandler.CallGameWonEvent("VICTORY!", "DEBUG WIN!");
    }
}
