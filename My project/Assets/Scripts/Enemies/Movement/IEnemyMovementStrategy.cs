using UnityEngine;

public interface IEnemyMovementStrategy
{
    void Initialize(Transform entityTransform);
    bool Move(Transform entityTransform, float moveSpeed);
    void ResetMovement();
}