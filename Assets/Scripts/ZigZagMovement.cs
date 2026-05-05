using UnityEngine;

public class ZigZagMovement : IEnemyMovement
{
    private float frequency = 4f;
    private float magnitude = 9f;
    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        if (target == null) return;
        Vector3 direction = (target.position - enemyTransform.position).normalized;
        Vector3 cross = Vector3.Cross(direction, Vector3.up);
        Vector3 zigzag = cross * Mathf.Sin(Time.time * frequency) * magnitude;
        enemyTransform.position += (direction * speed + zigzag) * Time.deltaTime;
        enemyTransform.LookAt(target);
    }
}