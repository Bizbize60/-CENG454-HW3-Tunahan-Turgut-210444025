using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour 
{
    public Color hoverHighlightColor;
    public Vector3 constructionOffset;

    private GameObject deployedTurret;
    private Renderer meshRenderer;
    private Color originalColor;

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        originalColor = meshRenderer.material.color;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (deployedTurret != null)
        {
            Debug.Log("Area occupied! - TODO: Display on screen.");
            return;
        }

        deployedTurret = ConstructionManager.instance.ConstructTurret(
            transform.position + constructionOffset,
            transform.rotation
        );
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        meshRenderer.material.color = hoverHighlightColor;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = originalColor;
    }
}