using UnityEngine;

public abstract class Enemy : MonoBehaviour
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
        OnAwake();
    }

    private void OnEnable()
    {
        pathStrategy?.Initialize(transform);
        OnSpawned();
    }

    private void Update()
    {
        if (pathStrategy == null || isDestroyed) return;

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
        OnReset();
    }

    public void TakeDamage(float damageTaken)
    {
    if (isDestroyed) return;
    float finalDamage = ModifyIncomingDamage(damageTaken); // önce modifiye et
    activeHealth -= finalDamage;
    OnDamageTaken(finalDamage);

    if (activeHealth <= 0f)
        ExecuteDeath();
    }

    protected virtual float ModifyIncomingDamage(float damage)
    {
        return damage; 
    }

    private void ExecuteDeath()
    {
        isDestroyed = true;
        EnemyEvents.RaiseEnemyDied(this);
        OnDeath();
        gameObject.SetActive(false);
    }

    protected virtual void OnAwake() { }
    protected virtual void OnSpawned() { }
    protected virtual void OnReset() { }
    protected virtual void OnDamageTaken(float damage) { }
    protected virtual void OnDeath() { }
}