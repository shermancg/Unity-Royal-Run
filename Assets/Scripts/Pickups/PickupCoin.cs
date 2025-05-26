using UnityEngine;

public class PickupCoin : Pickup
{
    [Header("References")]
    [SerializeField] ScoreManager scoreManager; // Reference to the ScoreManager to update the score

    [Header("Coin Settings")]
    [SerializeField] int scoreValue = 1;

    void Start()
    {
        // This is actually dangerous because we're finding the ScoreManager for EVERY coin spawned.
        // It's better to have a single ScoreManager in the scene and reference it directly.
        scoreManager = FindAnyObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene. Please add a ScoreManager component.");
        }
    }
    protected override void OnPickup()
    {
        scoreManager.IncreaseScore(scoreValue); // Increase the score by the coin's value
    }
}
