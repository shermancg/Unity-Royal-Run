using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] TMPro.TMP_Text timeText;
    [SerializeField] GameObject gameOverTextGO;

    [Header("Game Settings")]
    [SerializeField] float startTime = 10f;

    bool gameOver = false;
    float timeLeft;

    // public bool GameOver { get { return gameOver; } } //This is a property to check if the game is over
    public bool GameOver => gameOver; // Using expression-bodied property for simplicity. Same as above.

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        if (gameOver) return; 
        DecreaseTime();
    }


    private void DecreaseTime()
    {
        timeLeft -= Time.deltaTime; // Decrease time left by the time passed since last frame
        timeText.text = "Time: " + timeLeft.ToString("F1"); // Update the time text

        if (timeLeft <= 0f)
        {
            EndTheGame();
        }
    }

    private void EndTheGame()
    {
        gameOver = true;
        gameOverTextGO.SetActive(true);
        timeLeft = 0f;
        Time.timeScale = 0.1f;
        playerController.enabled = false;
    }
}