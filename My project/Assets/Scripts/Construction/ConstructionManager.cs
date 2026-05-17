using UnityEngine;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager instance;

    void Awake() 
    {
        if (instance != null)
        {
            Debug.LogError("More than one ConstructionManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject constructionEffect;
    public GameObject liquidationEffect;
}