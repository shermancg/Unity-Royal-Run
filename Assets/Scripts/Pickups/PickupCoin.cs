using UnityEngine;

public class PickupCoin : Pickup
{

    [Header("Coin Settings")]
    [SerializeField] int scoreValue = 1;

    ScoreManager scoreManager; // Reference to the ScoreManager to update the score

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
    
    protected override void OnPickup()
    {
        scoreManager.IncreaseScore(scoreValue); // Increase the score by the coin's value
    }



}
