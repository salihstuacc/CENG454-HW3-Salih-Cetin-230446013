using UnityEngine;
public interface IEnemyMovement
{
    void Move(Transform enemyTransform, Transform target, float speed);
}