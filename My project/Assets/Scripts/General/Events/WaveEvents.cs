using System;

public static class WaveEvents
{
    public static event Action<float> OnCountdownChanged;
    public static event Action<int> OnWaveStarted;
    public static event Action<Enemy> OnEnemySpawned;

    public static void RaiseCountdownChanged(float timerValue)
    {
        OnCountdownChanged?.Invoke(timerValue);
    }

    public static void RaiseWaveStarted(int currentWave)
    {
        OnWaveStarted?.Invoke(currentWave);
    }

    public static void RaiseEnemySpawned(Enemy targetEntity)
    {
        OnEnemySpawned?.Invoke(targetEntity);
    }
}