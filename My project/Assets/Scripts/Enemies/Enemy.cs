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
        // Obje havuzdan (pool) her çekildiğinde stratejiyi baştan başlatır
        pathStrategy?.Initialize(transform);
    }
}