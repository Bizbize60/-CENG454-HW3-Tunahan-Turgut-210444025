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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}