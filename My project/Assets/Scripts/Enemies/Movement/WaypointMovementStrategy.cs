using UnityEngine;

public class WaypointMovementStrategy : MonoBehaviour, IEnemyMovementStrategy
{
    private Transform currentTargetNode;
    private int currentNodeIndex = 0;
    
    private readonly float reachDistance = 0.35f;

    public void Initialize(Transform entityTransform)
    {
        ResetMovement();

        if (PathNodes.nodes != null && PathNodes.nodes.Length > 0)
        {
            currentTargetNode = PathNodes.nodes[0];
        }
    }

    public bool Move(Transform entityTransform, float moveSpeed)
    {
        if (currentTargetNode == null)
        {
            return true;
        }

        Vector3 moveDirection = currentTargetNode.position - entityTransform.position;
        entityTransform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(entityTransform.position, currentTargetNode.position) <= reachDistance)
        {
            return AcquireNextNode();
        }

        return false;
    }

    private bool AcquireNextNode()
    {
       
        if (currentNodeIndex >= PathNodes.nodes.Length - 1)
        {
            return true; 
        }

        currentNodeIndex++;
        currentTargetNode = PathNodes.nodes[currentNodeIndex];
        return false;
    }

    public void ResetMovement()
    {
        currentNodeIndex = 0;
        currentTargetNode = null;
    }
}