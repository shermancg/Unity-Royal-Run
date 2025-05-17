using System.Collections.Generic;
using UnityEngine;

public class ChunkDetails : MonoBehaviour
{
    [SerializeField] private GameObject fencePrefab;
    [SerializeField] float[] laneArray = { -2.5f, 0, 2.5f };

    void Start()
    {
        SpawnFence();
    }

    void SpawnFence()
    {
        // This list contains the available lanes for spawning fences
        // The list is 0, 1, 2 because the laneArray array has 3 entries, so it can assign 0 (lane 1), 1 (lane 2), or 2 (lane 3) to the availableLanes list
        List<int> availableLanes = new List<int> { 0, 1, 2 };

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

            // This assigns a random lane from the availableLanes list to the randomLaneIndex variable
            int randomLaneIndex = Random.Range(0, availableLanes.Count);

            // This int is what actually declares the selected lane, which is the lane that will be used to spawn the fence by taking the randomLaneIndex from the availableLanes list
            int selectedLane = availableLanes[randomLaneIndex];

            // This removes the selected lane from the availableLanes list so that it can't be used again
            // This is important because if you don't remove the lane, it will spawn fences in the same lane multiple times
            availableLanes.RemoveAt(randomLaneIndex);

            // This is the spawn position of the fence, which is the laneArray array at the selectedLane index
            Vector3 spawnPos = new Vector3(laneArray[selectedLane], transform.position.y, transform.position.z);

            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

}
