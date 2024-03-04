using UnityEngine;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPrefab;
    public int verticalPointNum = 16;
    public int horizontalPointNum = 23;

    public List<GameObject> spawnPoints = new List<GameObject>();
    public EnemyManager enemyManager;

    void Start()
    {
        SpawnObjects();
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
        enemyManager.SpawnEnemy();
    }

    void InstantiateSpawnPoint(Vector3 position)
    {
        GameObject newPoint = Instantiate(spawnPrefab, position, Quaternion.identity);
        newPoint.transform.parent = transform;
        spawnPoints.Add(newPoint);
    }

    public List<GameObject> GetSpawnPoints()
    {
        return spawnPoints;
    }


}
