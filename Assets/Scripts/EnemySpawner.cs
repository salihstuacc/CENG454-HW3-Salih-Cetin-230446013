using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPool pool;
    public Transform spawnPoint;
    public Transform coreTarget;

    [Header("Zorluk Ayarları")]
    public float startSpawnInterval = 3f;      
    public float minSpawnInterval = 0.5f;      
    public float spawnIntervalDecrease = 0.05f; 

    public float startHealth = 50f;            
    public float healthIncrease = 1f;          

    private float currentSpawnInterval;
    private float currentHealth;
    private bool isSpawning = true;

    void Start()
    {
        
        currentSpawnInterval = startSpawnInterval;
        currentHealth = startHealth;

        Core.OnCoreDestroyed += StopSpawning;
        StartCoroutine(SpawnRoutine());
    }

    void OnDisable()
    {
        Core.OnCoreDestroyed -= StopSpawning;
    }

    void StopSpawning()
    {
        isSpawning = false;
    }

    IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(currentSpawnInterval);
            
            GameObject enemyObj = pool.GetEnemy();
            if (enemyObj != null)
            {
                enemyObj.transform.position = spawnPoint.position;
                
                Enemy enemyScript = enemyObj.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.targetCore = coreTarget;
                    enemyScript.currentMovementType = (Random.value > 0.5f) ? Enemy.MovementType.Direct : Enemy.MovementType.ZigZag;
                    enemyScript.SetMovementStrategy(enemyScript.currentMovementType);
                    
                    
                    enemyScript.health = currentHealth;
                }
            }

          
            if (currentSpawnInterval > minSpawnInterval)
            {
                currentSpawnInterval -= spawnIntervalDecrease;
            }

            
            currentHealth += healthIncrease;
        }
    }
}