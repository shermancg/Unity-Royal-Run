using UnityEngine;

public class PickupApple : Pickup
{
    LevelGenerator levelGenerator;
    ObstacleSpawner obstacleSpawner;


    void Start()
    {
        // This works fine, but it's a bit of code bloat because we are adding a start to EVERY apple. The code bloat solution is too advanced for now
        levelGenerator = FindFirstObjectByType<LevelGenerator>();

        if (levelGenerator == null)
        {
            Debug.LogError("LevelGenerator not found in the scene.");
            return;
        }
        
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
        if (obstacleSpawner == null)
        {
            Debug.LogError("ObstacleSpawner not found in the scene.");
            return;
        }
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(levelGenerator.increaseSpeed); // Increase the chunk move speed on pickup
        // obstacleSpawner.AdjustSpawnInterval(obstacleSpawner.spawnFaster); // Decrease the spawn interval on pickup
        Debug.Log("Apple picked up!");
    }

}
