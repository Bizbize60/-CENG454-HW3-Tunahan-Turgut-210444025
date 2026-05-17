using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour {

    public Color hoverHighlightColor;
    public Color insufficientFundsColor;
    public Vector3 constructionOffset;

    
    public GameObject currentTurret;
    public TurretBlueprint currentBlueprint;
  

    private Renderer meshRenderer;
    private Color originalColor;

    private ConstructionManager constructionManager;

    void Start ()
    {
        meshRenderer = GetComponent<Renderer>();
        originalColor = meshRenderer.material.color;

        constructionManager = ConstructionManager.instance;
    }

    public Vector3 GetConstructionPosition ()
    {
        return transform.position + constructionOffset;
    }

    void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!constructionManager.CanConstruct) return;

        if (constructionManager.HasRequiredFunds)
        {
            meshRenderer.material.color = hoverHighlightColor;
        } 
        else
        {
            meshRenderer.material.color = insufficientFundsColor;
        }
    }

    void OnMouseExit ()
    {
        meshRenderer.material.color = originalColor;
    }

    void OnMouseDown ()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (currentTurret != null)
        {
            constructionManager.SelectGround(this);
            return;
        }

        if (!constructionManager.CanConstruct) return;

        ConstructTurret(constructionManager.GetTurretToConstruct());
    }

    void ConstructTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to construct that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetConstructionPosition(), Quaternion.identity);
        currentTurret = _turret;
        currentBlueprint = blueprint;

        GameObject effect = Instantiate(constructionManager.constructionEffect, GetConstructionPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret constructed!");
    }
}