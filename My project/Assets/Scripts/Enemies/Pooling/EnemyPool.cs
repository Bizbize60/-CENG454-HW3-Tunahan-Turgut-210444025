using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy entityPrefab;
    [SerializeField] private int startupPoolCapacity = 15;

    private readonly Queue<Enemy> entityQueue = new Queue<Enemy>();

    private void Awake()
    {
        for (int index = 0; index < startupPoolCapacity; index++)
        {
            InstantiateNewEntity();
        }
    }

    private Enemy InstantiateNewEntity()
    {
        Enemy newEntity = Instantiate(entityPrefab, transform);
        newEntity.gameObject.SetActive(false);
        entityQueue.Enqueue(newEntity);
        return newEntity;
    }

    
    public Enemy GetEnemy(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (entityQueue.Count == 0)
        {
            InstantiateNewEntity();
        }

        Enemy activeEntity = entityQueue.Dequeue();

        activeEntity.transform.position = spawnPosition;
        activeEntity.transform.rotation = spawnRotation;
        
        activeEntity.ResetEntity();
        activeEntity.gameObject.SetActive(true);

        return activeEntity;
    }

    public void ReturnEnemy(Enemy returningEntity)
    {
        returningEntity.gameObject.SetActive(false);
        entityQueue.Enqueue(returningEntity);
    }
}