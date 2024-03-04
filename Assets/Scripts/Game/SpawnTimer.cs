using System.Collections;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    public float startSpawnDelay = 2f;
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.5f;
    public float spawnIntervalDecrement = 0.1f; 

    private EnemyManager enemyManager;

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>(); 
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        float currentSpawnInterval = initialSpawnInterval;

        while (currentSpawnInterval >= minSpawnInterval)
        {
            enemyManager.SpawnEnemy();

            currentSpawnInterval -= spawnIntervalDecrement;

            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval);

            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }
  
}
