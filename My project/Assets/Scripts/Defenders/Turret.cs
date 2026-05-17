using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

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

    private ITargetingStrategy targetingStrategy;

    private void Awake()
    {
        targetingStrategy = GetComponent<ITargetingStrategy>();
    }

    private void Start() 
    {
        InvokeRepeating(nameof(ScanForTargets), 0f, 0.5f);
    }
    
    private void ScanForTargets()
    {
        if (targetingStrategy == null)
        {
            currentTarget = null;
            return;
        }

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(targetTag);
        List<Enemy> validEnemies = new List<Enemy>();

        foreach (GameObject go in enemyObjects)
        {
            Enemy enemy = go.GetComponent<Enemy>();
            if (enemy != null) 
            {
                validEnemies.Add(enemy);
            }
        }

        currentTarget = targetingStrategy.FindTarget(transform.position, attackRange, validEnemies);
    }

    private void Update() 
    {
        if (currentTarget == null)
        {
            return;
        }

        if (!currentTarget.gameObject.activeInHierarchy)
        {
            currentTarget = null;
            return;
        }

        Vector3 directionToTarget = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        Vector3 smoothedRotation = Quaternion.Lerp(rotatingHead.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotatingHead.rotation = Quaternion.Euler(0f, smoothedRotation.y, 0f);

        if (fireTimer <= 0f)
        {
            FireProjectile();
            fireTimer = 1f / fireRate;
        }

        fireTimer -= Time.deltaTime;
    }

    private void FireProjectile()
    {
        Bullet activeProjectile = bulletPool.GetBullet(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        activeProjectile.Seek(currentTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}