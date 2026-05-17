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
}