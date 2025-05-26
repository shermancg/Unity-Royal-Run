using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public TMPro.TMP_Text scoreText; 

    int score = 0;

    public void IncreaseScore(int amount)
    {
        score += amount; 
        
        scoreText.text = "Score: " + score;
    }


}
