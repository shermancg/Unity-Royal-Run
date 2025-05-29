using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;

    [Header("Checkpoint Settings")]
    // [SerializeField] float checkpointTimeIncrease = 5f; 

    const string playerTag = "Player";

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.Timeleft += gameManager.CheckpointTimeIncrease; // Increase the time left by 5 seconds when the player reaches a checkpoint
            Debug.Log("Checkpoint reached!");
        }
    }
}
