using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private IEnemy entityPrefab;
    [SerializeField] private int startupPoolCapacity = 15;

    private readonly Queue<IEnemy> entityQueue = new Queue<IEnemy>();

    private void Awake()
    {
        for (int index = 0; index < startupPoolCapacity; index++)
        {
            InstantiateNewEntity();
        }
    }

    private Enemy InstantiateNewEntity()
    {
        IEnemy newEntity = Instantiate(entityPrefab, transform);
        newEntity.gameObject.SetActive(false);
        entityQueue.Enqueue(newEntity);
        return newEntity;
    }

    
    public IEnemy GetEnemy(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (entityQueue.Count == 0)
        {
            InstantiateNewEntity();
        }

        IEnemy activeEntity = entityQueue.Dequeue();

        activeEntity.transform.position = spawnPosition;
        activeEntity.transform.rotation = spawnRotation;
        
        activeEntity.ResetEntity();
        activeEntity.gameObject.SetActive(true);

        return activeEntity;
    }

    public void ReturnEnemy(IEnemy returningEntity)
    {
        returningEntity.gameObject.SetActive(false);
        entityQueue.Enqueue(returningEntity);
    }
}