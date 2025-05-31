using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraControls cameraControls; 
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkPointChunkPrefab;
    [SerializeField] Transform chunkParent; // Parent object for the chunks
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Gen Settings")]
    [SerializeField] int startingChunksAmount = 10; // Number of chunks to instantiate at the start
    [Tooltip("Don't change this unless you also change the chunk prefab's length.")]
    [SerializeField] float chunkLength = 10f; // Length of each chunk
    [SerializeField] int checkPointChunkInterval = 8; // Interval at which checkpoint chunks are spawned
    [SerializeField] float moveSpeed = 7f; // Speed at which the chunks move
    [SerializeField] float minMoveSpeed = 5f;
    [SerializeField] public float reduceSpeed = -2f;
    [SerializeField] public float increaseSpeed = 2f;
    float gravityLimiter = -9.81f;
    int chunksSpawnedCount = 0; // Counter for the number of chunks spawned

    List<GameObject> chunks = new List<GameObject>(); // List to hold the instantiated chunks

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks(); // Call the method to move the chunks
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            float spawnPositionZ = i * chunkLength; // Calculate the spawn position based on the chunk length
            InstantiateChunk(spawnPositionZ); // Instantiate the chunk at the calculated position
        }
    }

    public void ChangeChunkMoveSpeed(float newSpeed)
    {
        moveSpeed += newSpeed; // Update the move speed

        if (moveSpeed < minMoveSpeed) // Check if the new speed is below the minimum
        {
            moveSpeed = minMoveSpeed; // Set to minimum speed
        }

        ManageGravity(newSpeed);

        cameraControls.ChangeFOV(newSpeed); // Call the method to change the camera's Field of View
    }

    private void ManageGravity(float newSpeed)
    {
        float newGravityY = Physics.gravity.y - newSpeed;
        float newGravityZ = Physics.gravity.z - newSpeed;

        // Clamp so gravity.y and gravity.z never go above (less negative than) -9.81
        if (newGravityY > gravityLimiter)
        {
            newGravityY = gravityLimiter;
        }
        if (newGravityZ > gravityLimiter)
        {
            newGravityZ = gravityLimiter;
        }

        Physics.gravity = new Vector3(Physics.gravity.x, newGravityY, newGravityZ);
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.position += Vector3.back * moveSpeed * Time.deltaTime; // Move each chunk backward

            if (chunk.transform.position.z <= Camera.main.transform.position.z -chunkLength) // Check if the chunk is out of view
            {
                Destroy(chunk); // Destroy the chunk
                chunks.Remove(chunk); // Remove it from the list
                SpawnNewChunk(); // Call the method to spawn a new chunk if needed
            }
        }
    }

    private void SpawnNewChunk()
    {
        float spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength; // Calculate the spawn position for the new chunk
        InstantiateChunk(spawnPositionZ); // Instantiate the new chunk
    }

    private void InstantiateChunk(float spawnPositionZ)
    {
        chunksSpawnedCount++; // Increase the count of spawned chunks by 1

        GameObject pickAChunk;

        if (chunksSpawnedCount % checkPointChunkInterval == 0) // Every 8 chunks, spawn a checkpoint chunk
        {
            pickAChunk = checkPointChunkPrefab;
        }
        else
        {
            pickAChunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        GameObject newChunkGO = Instantiate(pickAChunk, new Vector3(0, 0, spawnPositionZ), Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO); // Add the new chunk to the list

        ChunkDetails newChunk = newChunkGO.GetComponent<ChunkDetails>();
        newChunk.Init(this, scoreManager);

    }

}
