using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    private float lastHealth = 100f; 
    
    private int currentScore = 0; 

    private void OnEnable()
    {
        Core.OnCoreHealthChanged += HandleHealthChanged; 
        Core.OnCoreDestroyed += HandleGameOver;
        
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        Core.OnCoreHealthChanged -= HandleHealthChanged; 
        Core.OnCoreDestroyed -= HandleGameOver;
        
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }

    void HandleEnemyKilled()
    {
        currentScore++; 
    }

    void HandleHealthChanged(float currentHealth)
    {
        float damageTaken = lastHealth - currentHealth;
        lastHealth = currentHealth;
        
        Debug.Log($"{damageTaken} hasar alındı, kalan can {currentHealth}");
    }

    void HandleGameOver()
    {

        Debug.Log($"OYUN BİTTİ! Çekirdek parçalandı ve savunma düştü. Toplam Skor: {currentScore} düşman yok edildi!");
        Time.timeScale = 0f; 
    }
}