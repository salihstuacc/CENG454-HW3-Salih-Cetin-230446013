using UnityEngine;
public class DirectMovement : IEnemyMovement
{
    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        if (target == null) return;
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target.position, speed * Time.deltaTime);
        enemyTransform.LookAt(target);
    }
}