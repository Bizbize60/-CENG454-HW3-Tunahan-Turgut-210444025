using UnityEngine;

public class PathNodes : MonoBehaviour
{
    public static Transform[] nodes;

    private void Awake()
    {
        nodes = new Transform[transform.childCount];
        
        for (int index = 0; index < nodes.Length; index++)
        {
            nodes[index] = transform.GetChild(index);
        }
    }
}