using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    [SerializeField] private EnemyPool monsterPool;
    [SerializeField] private Transform spawnOrigin;
    
    [SerializeField] private float waveInterval = 7.5f;
    [SerializeField] private float spawnInterval = 0.3f; 
    
    private float currentTimer = 4f;
    private int currentWave = 0; 

    private IEnumerator GenerateWave()
    {
        currentWave++;
        
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
    }

    private void InstantiateEntity()
    {
        Enemy monster = monsterPool.GetEnemy(spawnOrigin.position, spawnOrigin.rotation);
    }
}