using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VInspector;

public class ObstacleSpawner : MonoBehaviour
{
    // [SerializeField] GameObject obstaclePrefab;
    [SerializeField] List<GameObject> obstaclePrefabs;
    [SerializeField] Transform ObstacleParent; // Parent object for the obstacles
    [SerializeField] float spawnIntervalDelay = 1.5f;
    // [SerializeField] float spawnIntervalMinDelay = 1f; 
    // public float spawnSlower = 0.1f;
    // public float spawnFaster = -0.1f;
    [SerializeField] int spawnCap = 10;
    [SerializeField] float spawnWidth = 5f; // Width of the spawn area

    int obstaclesSpawned = 0;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    // public void AdjustSpawnInterval(float intervalAdjustment)
    // {
    //     spawnIntervalDelay -= intervalAdjustment;
    //     if (spawnIntervalDelay < spawnIntervalMinDelay) // Prevent the spawn interval from going too fast
    //     {
    //         spawnIntervalDelay = spawnIntervalMinDelay;
    //     }
    // }

    IEnumerator SpawnObstacles()
    {
        while (obstaclesSpawned < spawnCap)
        {
            yield return new WaitForSeconds(spawnIntervalDelay);
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // Randomly select an obstacle prefab from the list
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        // Random X position within the spawn width
        Vector3 spawnPositionXVariance = new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);

        Instantiate(obstaclePrefab, transform.position + spawnPositionXVariance, Random.rotation, ObstacleParent);
        obstaclesSpawned++;
    }
}
