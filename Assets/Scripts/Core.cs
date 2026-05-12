using System;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public bool IsDead => health <= 0; 
    public static event Action<float> OnCoreHealthChanged;
    public static event Action OnCoreDestroyed;

    public void TakeDamage(float damageAmount)
    {
        if (IsDead) return; 

        health -= damageAmount;
        OnCoreHealthChanged?.Invoke(health);

        if (IsDead) 
        {
            OnCoreDestroyed?.Invoke();
            gameObject.SetActive(false); 
        }
    }
}