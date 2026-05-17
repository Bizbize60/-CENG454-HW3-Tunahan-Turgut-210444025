using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 10f;
    public float baseHealth = 100f;
    
    private float activeHealth;
    private bool isDestroyed = false;
    
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
        activeHealth = baseHealth;
        isDestroyed = false;
        pathStrategy?.ResetMovement();
    }


    public void TakeDamage(float damageTaken)
    {
        if (isDestroyed)
        {
            return;
        }
        
        activeHealth -= damageTaken;
        
        if (activeHealth <= 0f)
        {
            ExecuteDeath();
        }
    }


    private void ExecuteDeath()
    {
        isDestroyed = true;
        EnemyEvents.RaiseEnemyDied(this);
        gameObject.SetActive(false);
    }
}