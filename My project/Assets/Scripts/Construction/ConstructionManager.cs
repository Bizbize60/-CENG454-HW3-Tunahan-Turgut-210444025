using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public static ConstructionManager instance;

    public GameObject standardTurretPrefab;
    [SerializeField] private BulletPool projectilePool;

    private GameObject turretToConstruct;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one ConstructionManager in scene!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        turretToConstruct = standardTurretPrefab;
    }

    public GameObject GetTurretToConstruct()
    {
        return turretToConstruct;
    }

    public GameObject ConstructTurret(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Tareti sahneye yerleştirir
        GameObject turretInstance = Instantiate(turretToConstruct, spawnPosition, spawnRotation);

        // Taret bileşenine erişip mermi havuzunu (pool) bağlar
        Turret turretScript = turretInstance.GetComponent<Turret>();
        if (turretScript != null)
        {
            turretScript.SetBulletPool(projectilePool);
        }

        return turretInstance;
    }
}