using UnityEngine;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager instance;

    void Awake() { /* Singleton Kontrolü */ }

    public GameObject constructionEffect;
    public GameObject liquidationEffect;

    private TurretBlueprint turretToConstruct;
    private Ground selectedGround;

    public GroundUI groundUI;

    public bool CanConstruct { get { return turretToConstruct != null; } }
    public bool HasRequiredFunds { get { return PlayerStats.Money >= turretToConstruct.cost; } }

    public void SelectGround(Ground ground)
    {
        if (selectedGround == ground)
        {
            DeselectGround();
            return;
        }

        selectedGround = ground;
        turretToConstruct = null;

        groundUI.SetTarget(ground);
    }

    public void DeselectGround()
    {
        selectedGround = null;
        groundUI.Hide();
    }

    public void SelectTurretToConstruct(TurretBlueprint turret)
    {
        turretToConstruct = turret;
        DeselectGround();
    }

    public TurretBlueprint GetTurretToConstruct()
    {
        return turretToConstruct;
    }
}