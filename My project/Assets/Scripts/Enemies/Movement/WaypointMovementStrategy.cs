using UnityEngine;

public class WaypointMovementStrategy : MonoBehaviour, IEnemyMovementStrategy
{
    private Transform currentTargetNode;
    private int currentNodeIndex = 0;

    public void Initialize(Transform entityTransform)
    {
        ResetMovement();
        if (PathNodes.nodes != null && PathNodes.nodes.Length > 0)
        {
            currentTargetNode = PathNodes.nodes[0];
        }
    }

    public void ResetMovement()
    {
        currentNodeIndex = 0;
        currentTargetNode = null;
    }
}