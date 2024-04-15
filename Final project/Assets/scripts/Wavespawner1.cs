using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavespawner1 : MonoBehaviour
{
    public GameObject bossEnemyPrefab; 
    public int numberOfBosses = 3;
    public float spawnInterval = 30f; 

    void Start()
    {
       
        StartCoroutine(SpawnBossEnemies());
    }

    IEnumerator SpawnBossEnemies()
    {
        while (true)
        {

            for (int i = 0; i < numberOfBosses; i++)
            {
                Instantiate(bossEnemyPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f); 
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}