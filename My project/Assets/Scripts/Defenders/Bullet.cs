using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float flightSpeed = 75f;
    public float impactDamage = 45f;
    public GameObject hitVFX;

    private Transform currentTarget;
    private BulletPool assignedPool;

    public void SetPool(BulletPool bulletPool)
    {
        assignedPool = bulletPool;
    }
    
    public void Seek(Transform newTarget)
    {
        currentTarget = newTarget;
    }

    private void Update() 
    {
        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            ReleaseToPool();
            return;
        }

        Vector3 directionToTarget = currentTarget.position - transform.position;
        float frameTravelDistance = flightSpeed * Time.deltaTime;

        if (directionToTarget.magnitude <= frameTravelDistance)
        {
            TriggerImpact();
            return;
        }

        transform.Translate(directionToTarget.normalized * frameTravelDistance, Space.World);
    }

    private void TriggerImpact()
    {
        if (hitVFX != null)
        {
            GameObject vfxInstance = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfxInstance, 1.5f); 
        }
        
        
        Enemy targetEnemy = currentTarget.GetComponent<Enemy>();
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(impactDamage);
        }

        ReleaseToPool();
    }

    private void ReleaseToPool()
    {
        currentTarget = null;
        
        if (assignedPool != null)
        {
            assignedPool.ReturnBullet(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}