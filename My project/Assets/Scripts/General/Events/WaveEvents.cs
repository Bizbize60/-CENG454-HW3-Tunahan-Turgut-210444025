using System;

public static class WaveEvents
{
    public static event Action<float> OnCountdownChanged;
    public static event Action<int> OnWaveStarted;
    public static event Action<Enemy> OnEnemySpawned;
}