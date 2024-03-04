using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject[] enemies;
    public SpawnPointManager spawnPointManager;
    public List<GameObject> meleeList = new List<GameObject>();
    public List<GameObject> rangedList = new List<GameObject>();
    bool fearActive;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SpawnEnemy()
    {
        var spawnPoints = spawnPointManager.GetSpawnPoints();
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points found!");
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int enemyIndex = Random.Range(0, enemies.Length);

        var enemy = Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
        if (enemyIndex == 0)
        {
            AddMelee(enemy);
            return;
        }
        AddRanged(enemy);
    }
    public void AddMelee(GameObject enemy)
    {
        for (int i = 0; i < meleeList.Count; i++)
        {
            if (meleeList[i] == null)
            {
                meleeList[i] = enemy;
                return;
            }
        }
        meleeList.Add(enemy);
        if (fearActive)
        {
            enemy.GetComponent<MeleeMovement>().isFearActive = true;
        }
    }
    public void AddRanged(GameObject enemy)
    {
        for (int i = 0; i < rangedList.Count; i++)
        {
            if (rangedList[i] == null)
            {
                rangedList[i] = enemy;
                return;
            }
        }
        rangedList.Add(enemy);
        if (fearActive)
        {
            enemy.GetComponent<RangedMovement>().isFearActive = true;
        }
    }

    public void ActivateFear()
    {
        fearActive = true;
        foreach (var enemy in rangedList)
        {
            enemy.GetComponent<RangedMovement>().isFearActive = true;
        } 
        foreach (var enemy in meleeList)
        {
            enemy.GetComponent<MeleeMovement>().isFearActive = true;
        }
        StartCoroutine(DeactivateFear());
    }

    IEnumerator DeactivateFear()
    {
        fearActive = false;
        yield return new WaitForSeconds(1.5f);
        foreach (var enemy in rangedList)
        {
            enemy.GetComponent<RangedMovement>().isFearActive = false;
        }
        foreach (var enemy in meleeList)
        {
            enemy.GetComponent<MeleeMovement>().isFearActive = false;
        }
    }
}
