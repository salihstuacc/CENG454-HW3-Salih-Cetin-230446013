using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    private Transform target;
    private void OnEnable()
    {
        target = null; 
    }
    public void FireAt(Transform newTarget)
    {
        target = newTarget;
    }
    void Update()
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}