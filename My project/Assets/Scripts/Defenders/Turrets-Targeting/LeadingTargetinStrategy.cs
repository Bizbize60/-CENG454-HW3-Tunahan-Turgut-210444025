using UnityEngine;
using System.Collections.Generic;

public class LeadingTargetingStrategy : MonoBehaviour, ITargetingStrategy
{
    public Transform FindTarget(Vector3 searchOrigin, float detectionRange, IReadOnlyList<Enemy> potentialTargets)
    {
        Transform leadingTarget = null;
        float minimumDistanceToFinish = Mathf.Infinity;

        foreach (Enemy targetCandidate in potentialTargets)
        {
            if (targetCandidate == null || !targetCandidate.gameObject.activeInHierarchy) continue;

            float distanceToTurret = Vector3.Distance(searchOrigin, targetCandidate.transform.position);
            if (distanceToTurret > detectionRange) continue;
            Transform finalDestination = PathNodes.nodes[PathNodes.nodes.Length - 1];
            float distanceLeftToTravel = Vector3.Distance(targetCandidate.transform.position, finalDestination.position);
            if (distanceLeftToTravel < minimumDistanceToFinish)
            {
                minimumDistanceToFinish = distanceLeftToTravel;
                leadingTarget = targetCandidate.transform;
            }
        }

        return leadingTarget;
    }
}