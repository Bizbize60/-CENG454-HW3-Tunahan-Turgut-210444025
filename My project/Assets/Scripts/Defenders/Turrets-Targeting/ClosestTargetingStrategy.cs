using UnityEngine;
using System.Collections.Generic;

public class ClosestTargetingStrategy : MonoBehaviour, ITargetingStrategy
{
    public Transform FindTarget(Vector3 searchOrigin, float detectionRange, IReadOnlyList<Enemy> potentialTargets)
    {
        Transform closestTargetFound = null;
        float minDistanceFound = Mathf.Infinity;

        foreach (Enemy targetCandidate in potentialTargets)
        {
            if (targetCandidate == null || !targetCandidate.gameObject.activeInHierarchy) continue;

            float distanceToTarget = Vector3.Distance(searchOrigin, targetCandidate.transform.position);

            if (distanceToTarget < minDistanceFound && distanceToTarget <= detectionRange)
            {
                minDistanceFound = distanceToTarget;
                closestTargetFound = targetCandidate.transform;
            }
        }

        return closestTargetFound;
    }
}