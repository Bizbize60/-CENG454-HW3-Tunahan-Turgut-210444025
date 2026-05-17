using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 10f;

    private IEnemyMovementStrategy pathStrategy;

    private void Awake()
    {
        pathStrategy = GetComponent<IEnemyMovementStrategy>();
    }

    private void OnEnable()
    {
        pathStrategy?.Initialize(transform);
    }

    private void Update()
    {
        if (pathStrategy == null)
        {
            return;
        }

        bool hasReachedDestination = pathStrategy.Move(transform, moveSpeed);

        if (hasReachedDestination)
        {
            EnemyEvents.RaiseEnemyReachedEnd(this);
            gameObject.SetActive(false);
        }
    }

    public void ResetEntity()
    {
        pathStrategy?.ResetMovement();
    }
}