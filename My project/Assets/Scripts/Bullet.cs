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
}