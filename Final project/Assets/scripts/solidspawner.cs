using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solidspawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject zombiePrefab;
    public Transform playerTransform;
    public float ghostSpawnRadius = 6f;
    public float zombieSpawnRadius = 6f;
    public Vector2 spawnRange = new Vector2(25f, 25f);
    public float ghostSpawnIntervalMin = 5f;
    public float ghostSpawnIntervalMax = 10f;
    public float zombieSpawnIntervalMin = 8f;
    public float zombieSpawnIntervalMax = 12f;
    public float timer = 0f;

    void Start()
    {
        StartCoroutine(SpawnGhostsRandomly());
    }

    IEnumerator SpawnGhostsRandomly()
    {
        while (timer < 40f)
        {
            yield return new WaitForSeconds(Random.Range(ghostSpawnIntervalMin, ghostSpawnIntervalMax));

            Vector3 spawnPosition = GetRandomSpawnPosition(ghostSpawnRadius);
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            }
        }

        StartCoroutine(SpawnZombiesRandomly());
    }

    IEnumerator SpawnZombiesRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(zombieSpawnIntervalMin, zombieSpawnIntervalMax));

            Vector3 spawnPosition = GetRandomSpawnPosition(zombieSpawnRadius);
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition(float radius)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomDirection.x, randomDirection.y, 0f) * Random.Range(6f + radius, spawnRange.x);
        if (Vector3.Distance(spawnPosition, playerTransform.position) < 6f)
        {
            return Vector3.zero;
        }
        return spawnPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}