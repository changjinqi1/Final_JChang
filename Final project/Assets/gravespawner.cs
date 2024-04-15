using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravespawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public int numberOfObjects = 3;
    public float spawnRangeX = 25f;
    public float spawnRangeY = 25f;

    void Start()
    {

        SpawnObjects();
    }

    void SpawnObjects()
    {

        for (int i = 0; i < numberOfObjects; i++)
        {
 
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            float randomY = Random.Range(-spawnRangeY, spawnRangeY);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            Instantiate(objectPrefab, randomPosition, Quaternion.identity);
        }
    }
}