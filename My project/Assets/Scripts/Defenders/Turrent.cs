using UnityEngine;

public class Turret : MonoBehaviour 
{
    private Transform currentTarget;

    [Header("Attributes")]
    public float attackRange = 15f;
    public float fireRate = 1f;
    private float fireTimer = 0f;

    [Header("Unity Setup Fields")]
    public string targetTag = "Enemy";

    public Transform rotatingHead;
    public float rotationSpeed = 10f;

    [SerializeField] private BulletPool bulletPool;
    public Transform projectileSpawnPoint;

    private void Start() 
    {
        InvokeRepeating(nameof(ScanForTargets), 0f, 0.5f);
    }
    
    private void ScanForTargets()
    {
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in activeEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            currentTarget = nearestEnemy.transform;
        } 
        else
        {
            currentTarget = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}