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

    private void TriggerImpact() { }
    private void ReleaseToPool() { } 
}