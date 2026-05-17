using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    [SerializeField] private float waveInterval = 7.5f;
    private float currentTimer = 4f;

    private void Update()
    {
        if (currentTimer <= 0f)
        {
            currentTimer = waveInterval;
        }
        
        currentTimer -= Time.deltaTime;
    }
    private void InstantiateEntity()
    {
        Enemy monster = monsterPool.GetEnemy(spawnOrigin.position, spawnOrigin.rotation);
    }
}