using UnityEngine;

public class Tower : MonoBehaviour
{
    private IWeapon currentWeapon;
    private float nextFireTime;
    
    [Header("Connections")]
    public Transform target; 
    public ProjectilePool pool;

    void Start()
    {
        currentWeapon = new BasicWeapon();
    }

    void Update()
    {
        if (target != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentWeapon.GetCooldown();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) ApplyDamageUpgrade();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ApplySpeedUpgrade();
    }

    void Shoot()
    {
        GameObject bulletObj = pool.GetProjectile();
        bulletObj.transform.position = transform.position;
        
        Projectile proj = bulletObj.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.damage = currentWeapon.GetDamage(); 
            proj.FireAt(target);
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
}