using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0.6f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private Vector2 spawnRange = new Vector2(25f, 25f);
    [SerializeField] private float playerRadius = 6f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxEnemies = 200;

    private int currentEnemyCount = 0;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            if (currentEnemyCount < maxEnemies)
            {

                Vector2 spawnPosition = GetRandomSpawnPosition();

                if (Vector2.Distance(spawnPosition, playerTransform.position) > playerRadius)
                {
                    int rand = Random.Range(0, enemyPrefabs.Length);
                    GameObject enemyToSpawn = enemyPrefabs[rand];
                    Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                    currentEnemyCount++;
                }
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange.x, spawnRange.x);
        float randomY = Random.Range(-spawnRange.y, spawnRange.y);
        return new Vector2(randomX, randomY);
    }
}