using System;

public static class StaticEventHandler
{

    public static event Action<GameOverArgs> OnGameOver;
    public static event Action<GameWonArgs> OnGameWon;
    public static event Action<LevelUpArgs> OnLevelUp;
    public static event Action<LevelUpWeaponSelected> OnLevelUpWeaponSelected;

    public static void CallGameOverEvent(string title, string body)
    {
        OnGameOver?.Invoke(new GameOverArgs() { titleText = title, bodyText = body });
    }
    public static void CallGameWonEvent(string title, string body)
    {
        OnGameWon?.Invoke(new GameWonArgs() { titleText = title, bodyText = body });
    }

    public static void CallOnLevelUpEvent(int levelUp)
    {
        OnLevelUp?.Invoke(new LevelUpArgs() { level = levelUp});
    }
    public static void OnLevelUpWeaponSelectedEvent(WeaponData wd)
    {
        OnLevelUpWeaponSelected?.Invoke(new LevelUpWeaponSelected() { weaponData = wd});
    }


}


public class GameOverArgs : EventArgs
{
    public string titleText;
    public string bodyText;
}
public class GameWonArgs : EventArgs
{
    public string titleText;
    public string bodyText;
}

public class LevelUpArgs : EventArgs
{
    public int level;
}

public class LevelUpWeaponSelected: EventArgs
{
    public WeaponData weaponData;
}

