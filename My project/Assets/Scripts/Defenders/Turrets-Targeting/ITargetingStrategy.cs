using UnityEngine;
using System.Collections.Generic;

public interface ITargetingStrategy
{
    Transform FindTarget(Vector3 searchOrigin, float detectionRange, IReadOnlyList<Enemy> potentialTargets);
}