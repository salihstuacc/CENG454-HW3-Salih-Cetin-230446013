using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPool pool;
    public Transform spawnPoint;
    public Transform coreTarget;
    public float spawnInterval = 2f; 

    private bool isSpawning = true;

    void Start()
    {
        
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
            yield return new WaitForSeconds(spawnInterval);
            
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
                }
            }
        }
    }
}