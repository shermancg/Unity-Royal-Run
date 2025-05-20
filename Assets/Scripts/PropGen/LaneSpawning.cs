using System.Collections.Generic;
using UnityEngine;

public class ChunkDetails : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float[] laneArray = { -2.5f, 0, 2.5f };

    // This list contains the available lanes for spawning fences
    // The list is 0, 1, 2 because the laneArray array has 3 entries, so it can assign 0 (lane 1), 1 (lane 2), or 2 (lane 3) to the availableLanes list
    List<int> availableLanes = new List<int> { 0, 1, 2 };

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    void SpawnFences()
    {
        // This spawns 0, 1, or 2 fences per chunk
        // This is a RNG, it picks a random number between 0 and 2, so it will spawn 0, 1, or 2 fences per chunk. 
        // The 3 here is not included in the range because the Random.Range function is exclusive of the max value, and as an int it will only return 0, 1, or 2
        // We replaced 3 with laneArray.Length to make it more dynamic, so if we add more lanes in the future, we don't have to change this number
        int fenceSpawnCap = Random.Range(0, laneArray.Length);


        // It's easy to confuse that 0, 1, 2 in a list is actually 3 numbers because 0 in a list is the first entry, 
        // however, in Random.Range(0, 3) it is an *int* randomly spawning 0, 1, or 2 fences because the 3 is not included in the Random.Range function

        // This for loop will run fenceSpawnCap times, so 0, 1, or 2 times
        // As long as i is less than fenceSpawnCap, it will run
        for (int i = 0; i < fenceSpawnCap; i++)
        {
            // This is a failsafe to make sure that if there are no available lanes left, it will break out of the loop
            if (availableLanes.Count == 0) break;

            int selectedLane = SelectLane();

            // This is the spawn position of the fence, which is the laneArray array at the selectedLane index
            Vector3 spawnPos = new Vector3(laneArray[selectedLane], transform.position.y, transform.position.z);

            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        // Simple spawn chance for the apple, if Random.value (which is a random 0-1 value) is greater than the appleSpawnChance, it won't spawn and the rest of the code will be skipped
        // I have a feeling I'll want to use this in many instances in Necrolich and will probably make a function for it to make it easier to use
        if (Random.value > appleSpawnChance) return;

        // This is a failsafe to make sure that if there are no available lanes left, it will break out of the loop
        // I could have combined this with the above if statement, but I wanted to keep it separate for clarity, which would have looked like this:
        // if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;
        if (availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPos = new Vector3(laneArray[selectedLane], transform.position.y, transform.position.z);

        Instantiate(applePrefab, spawnPos, Quaternion.identity, this.transform);
    }

    void SpawnCoins()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        int maxCoins = 5;
        int coinsToSpawn = Random.Range(1, maxCoins + 1); // Randomly spawn 1 to 5 coins, the reason this has a +1 is because the max value is exclusive in Random.Range
        int firstCoinZPosOffset = 8; // Starts spawning coins at 8 units behind the transform (center) of the chunk

        // This will instantiate several coins in a row, so if coinsToSpawn is 5, it will spawn 5 coins in a row
        for (int i = 0; i < coinsToSpawn; i++)
        {
            float coinZPosOffset = firstCoinZPosOffset - (i * 2); // Each coin is 2 units behind the previous
            Vector3 spawnPos = new Vector3(laneArray[selectedLane], transform.position.y, (transform.position.z - firstCoinZPosOffset / 2 + coinZPosOffset));
            Instantiate(coinPrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    int SelectLane()
    {
        // This assigns a random lane from the availableLanes list to the randomLaneIndex variable
        int randomLaneIndex = Random.Range(0, availableLanes.Count);

        // This int is what actually declares the selected lane, which is the lane that will be used to spawn the fence by taking the randomLaneIndex from the availableLanes list
        int selectedLane = availableLanes[randomLaneIndex];

        // This removes the selected lane from the availableLanes list so that it can't be used again
        // This is important because if you don't remove the lane, it will spawn fences in the same lane multiple times
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }


}
