using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraControls cameraControls; 
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent; // Parent object for the chunks
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Gen Settings")]
    [SerializeField] int startingChunksAmount = 10; // Number of chunks to instantiate at the start
    [Tooltip("Don't change this unless you also change the chunk prefab's length.")]
    [SerializeField] float chunkLength = 10f; // Length of each chunk
    [SerializeField] float moveSpeed = 7f; // Speed at which the chunks move
    [SerializeField] float minMoveSpeed = 5f;
    [SerializeField] public float reduceSpeed = -2f;
    [SerializeField] public float increaseSpeed = 2f;
    float gravityLimiter = -9.81f;

    List<GameObject> chunks = new List<GameObject>(); // List to hold the instantiated chunks

    void Start()
    {
        SpawnStartingChunks();
    }


    void Update()
    {
        MoveChunks(); // Call the method to move the chunks
        // Debug.Log("Current move speed: " + moveSpeed); // Log the current move speed
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
        float newGravityZ = Physics.gravity.z - newSpeed;
        // Clamp so gravity.z never goes above (less negative than) -9.81
        if (newGravityZ > gravityLimiter)
        {
            newGravityZ = gravityLimiter;
        }
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            float spawnPositionZ = i * chunkLength; // Calculate the spawn position based on the chunk length
            InstantiateChunk(spawnPositionZ); // Instantiate the chunk at the calculated position
        }
    }

    private void InstantiateChunk(float spawnPositionZ)
    {
        GameObject newChunkGO = Instantiate(chunkPrefab, new Vector3(0, 0, spawnPositionZ), Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO); // Add the new chunk to the list

        ChunkDetails newChunk = newChunkGO.GetComponent<ChunkDetails>();
        newChunk.Init(this, scoreManager);
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

}
