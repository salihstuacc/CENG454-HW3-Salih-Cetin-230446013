using UnityEngine;

public class Tower : MonoBehaviour
{
    private IWeapon currentWeapon;
    private float nextFireTime;
    
    public float range = 15f;
    public float turnSpeed = 10f; 
    
    [Header("Connections")]
    public Transform target; 
    public ProjectilePool pool;
    public Transform firePoint; 

    void Start()
    {
        currentWeapon = new BasicWeapon();
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void UpdateTarget()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            if (!enemy.gameObject.activeInHierarchy || enemy.IsDead) continue;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0; 
            
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
            }

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + currentWeapon.GetCooldown();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) ApplyDamageUpgrade();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ApplySpeedUpgrade();
    }

    void Shoot()
    {
        if (pool == null) return;

        GameObject bulletObj = pool.GetProjectile();
        if (bulletObj != null)
        {
    
            bulletObj.transform.position = (firePoint != null) ? firePoint.position : transform.position;
            
            Projectile proj = bulletObj.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.damage = currentWeapon.GetDamage(); 
                proj.FireAt(target);
            }
        }
    }

    public void ApplyDamageUpgrade()
    {
        currentWeapon = new DamageUpgrade(currentWeapon);
        Debug.Log($"GÜÇLENDİRME: Hasar arttı! Yeni Hasar: {currentWeapon.GetDamage()}");
    }
    
    public void ApplySpeedUpgrade()
    {
        currentWeapon = new SpeedUpgrade(currentWeapon);
        Debug.Log($"GÜÇLENDİRME: Hız arttı! Yeni Bekleme Süresi: {currentWeapon.GetCooldown()}");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}