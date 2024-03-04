using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPrefab;

    public int verticalPointNum = 16;
    public int horizontalPointNum = 23;

    public List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject[] enemies;

    public float startSpawnDelay = 4f;
    public float spawnInterval = 3f; 
    public float spawnIntervalDecrement = 0.1f;
    void Start()
    {
        SpawnObjects();
        spawnPoints.Add(spawnPoint1);
        spawnPoints.Add(spawnPoint2);
    }
    void SpawnObjects()
    {
        Vector3 startPosLeft = spawnPoint1.transform.position;
        Vector3 startPosRight = spawnPoint2.transform.position;
        Vector3 offsetVertical = Vector3.up;
        Vector3 offsetHorizontal = Vector3.right;

        for (int i = 1; i < verticalPointNum; i++)
        {
            Vector3 positionLeft = startPosLeft - i * offsetVertical;
            Vector3 positionRight = startPosRight + i * offsetVertical;
            InstantiateSpawnPoint(positionLeft);
            InstantiateSpawnPoint(positionRight);
        }

        for (int i = 1; i < horizontalPointNum; i++)
        {
            Vector3 positionLeft = startPosLeft + i * offsetHorizontal;
            Vector3 positionRight = startPosRight - i * offsetHorizontal;
            InstantiateSpawnPoint(positionLeft);
            InstantiateSpawnPoint(positionRight);
        }
        SpawnEnemy();
        InvokeRepeating("SpawnEnemy", startSpawnDelay, spawnInterval);
    }

    void InstantiateSpawnPoint(Vector3 position)
    {
        GameObject newPoint = Instantiate(spawnPrefab, position, Quaternion.identity);
        newPoint.transform.parent = this.transform;
        spawnPoints.Add(newPoint);
    }

    void SpawnEnemy()
    {      
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int enemyIndex = Random.Range(0, enemies.Length);

        Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);

        spawnInterval -= spawnIntervalDecrement;

        spawnInterval = Mathf.Max(0.5f, spawnInterval);
    }

}
