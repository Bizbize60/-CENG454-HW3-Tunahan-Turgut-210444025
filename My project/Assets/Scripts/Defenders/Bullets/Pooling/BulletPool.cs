using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet projectilePrefab;
    [SerializeField] private int startupCapacity = 30;

    private readonly Queue<Bullet> projectileQueue = new Queue<Bullet>();

    private void Awake()
    {
        for (int index = 0; index < startupCapacity; index++)
        {
            InstantiateProjectile();
        }
    }

    private Bullet InstantiateProjectile()
    {
        Bullet newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.gameObject.SetActive(false);
        newProjectile.SetPool(this);
        projectileQueue.Enqueue(newProjectile);
        return newProjectile;
    }

    public Bullet GetBullet(Vector3 spawnPos, Quaternion spawnRot)
    {
        if (projectileQueue.Count == 0)
        {
            InstantiateProjectile();
        }

        Bullet activeProjectile = projectileQueue.Dequeue();

        activeProjectile.transform.position = spawnPos;
        activeProjectile.transform.rotation = spawnRot;
        activeProjectile.gameObject.SetActive(true);

        return activeProjectile;
    }

    public void ReturnBullet(Bullet returningProjectile)
    {
        returningProjectile.gameObject.SetActive(false);
        projectileQueue.Enqueue(returningProjectile);
    }
}