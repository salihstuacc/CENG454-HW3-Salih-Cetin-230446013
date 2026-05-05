using UnityEngine;
using System; 
public class Core : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public event Action<float> OnHealthChanged; 
    public event Action OnCoreDestroyed;
    public bool IsDead => currentHealth <= 0;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        if (IsDead) return;
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            OnCoreDestroyed?.Invoke();
        }
    }
}