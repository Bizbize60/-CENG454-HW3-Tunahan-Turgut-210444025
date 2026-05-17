using System;

public static class EnemyEvents
{
    public static event Action<Enemy> OnEnemyReachedEnd;

    public static void RaiseEnemyReachedEnd(Enemy targetEntity)
    {
        OnEnemyReachedEnd?.Invoke(targetEntity);
    }

    public static void RaiseEnemyDied(Enemy enemy)
    {
        OnEnemyDied?.Invoke(enemy);
    }
}