using System;

public static class FlagEvents
{
    public static Action EnableFlagScoreDisplay;

    public static void FireEnableFlagScoreDisplay()
    {
        EnableFlagScoreDisplay?.Invoke();
    }
    
    public static Action<int, int> UpdateFlagScore;

    public static void FireUpdateFlagScore(int red, int blue)
    {
        UpdateFlagScore?.Invoke(red, blue);
    }
}
