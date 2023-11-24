using System;

public static class StaticEventHandler
{

    public static event Action<GameOverArgs> OnGameOver;
    public static event Action<GameWonArgs> OnGameWon;
    public static event Action<LevelUpArgs> OnLevelUp;
    public static event Action<TripCompleteArgs> OnTripComplete;
    public static event Action<TripStartArgs> OnTripStarted;
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

    public static void CallOnTripCompleteEvent(int trip)
    {
        OnTripComplete?.Invoke(new TripCompleteArgs() { tripCompleted = trip });
    }

    public static void CallOnTripStartedEvent(int trip)
    {
        OnTripStarted?.Invoke(new TripStartArgs() { trip = trip });
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

public class TripCompleteArgs : EventArgs
{
    public int tripCompleted;
}

public class TripStartArgs : EventArgs
{
    public int trip;
}

