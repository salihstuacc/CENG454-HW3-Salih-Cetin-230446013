using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    private float lastHealth = 100f; 

    private void OnEnable()
    {
        Core.OnCoreHealthChanged += HandleHealthChanged; 
        Core.OnCoreDestroyed += HandleGameOver;
    }

    private void OnDisable()
    {
        Core.OnCoreHealthChanged -= HandleHealthChanged; 
        Core.OnCoreDestroyed -= HandleGameOver;
    }

    void HandleHealthChanged(float currentHealth)
    {
        float damageTaken = lastHealth - currentHealth;
        lastHealth = currentHealth;
        
        Debug.Log($"{damageTaken} hasar alındı, kalan can {currentHealth}");
    }

    void HandleGameOver()
    {
        Debug.Log("OYUN BİTTİ! Çekirdek parçalandı ve savunma düştü.");
        Time.timeScale = 0f; 
    }
}