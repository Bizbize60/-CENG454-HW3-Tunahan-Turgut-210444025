using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    [SerializeField] private EnemyPool armoredPool;
    [SerializeField] private EnemyPool lightweightPool;
    [SerializeField] private Transform spawnOrigin;

    [SerializeField] private float waveInterval = 7.5f;
    [SerializeField] private float spawnInterval = 0.3f;

    [Header("Spawn Ratios")]
    [Range(0f, 1f)]
    [SerializeField] private float armoredSpawnChance = 0.3f;

    private float currentTimer = 4f;
    private int currentWave = 0;

    private IEnumerator GenerateWave()
    {
        currentWave++;
        WaveEvents.RaiseWaveStarted(currentWave);

        for (int i = 0; i < currentWave; i++)
        {
            InstantiateEntity();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Update()
    {
        if (currentTimer <= 0f)
        {
            StartCoroutine(GenerateWave());
            currentTimer = waveInterval;
        }

        currentTimer -= Time.deltaTime;
        WaveEvents.RaiseCountdownChanged(currentTimer);
    }

    private void InstantiateEntity()
    {
        bool spawnArmored = Random.value < armoredSpawnChance;
        EnemyPool selectedPool = spawnArmored ? armoredPool : lightweightPool;

        Enemy monster = selectedPool.GetEnemy(spawnOrigin.position, spawnOrigin.rotation);
        WaveEvents.RaiseEnemySpawned(monster);
    }

    private void OnEntityFinishedPath(Enemy monster)
    {
        if (monster is ArmoredEnemy)
            armoredPool.ReturnEnemy(monster);
        else
            lightweightPool.ReturnEnemy(monster);
    }

    private void OnEnable()
    {
        EnemyEvents.OnEnemyReachedEnd += OnEntityFinishedPath;
        EnemyEvents.OnEnemyDied += OnEntityFinishedPath;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyReachedEnd -= OnEntityFinishedPath;
        EnemyEvents.OnEnemyDied -= OnEntityFinishedPath;
    }
}