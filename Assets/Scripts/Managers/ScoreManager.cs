using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public TMPro.TMP_Text scoreText; 
    [SerializeField] GameManager gameManager;

    int score = 0;

    public void IncreaseScore(int amount)
    {
        if (gameManager.GameOver) return;

        score += amount; 
        
        scoreText.text = "Score: " + score;
    }


}
