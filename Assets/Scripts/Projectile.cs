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
        Vector3 aimPoint = target.position + new Vector3(0, 0.7f, 0);
        transform.position = Vector3.MoveTowards(transform.position, aimPoint, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableTarget = other.GetComponent<IDamageable>();   
        if (damageableTarget != null)
        {
            damageableTarget.TakeDamage(damage);
            ProjectilePool pool = GetComponentInParent<ProjectilePool>();
            if (pool != null) pool.ReturnProjectile(this.gameObject);
            else gameObject.SetActive(false); 
        }
    }
}