using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] TMPro.TMP_Text timeText;
    [SerializeField] GameObject gameOverTextGO;
    [SerializeField] GameObject restartTextGO;

    [Header("Game Settings")]
    [SerializeField] float startTime = 10f;
    [SerializeField] float checkpointTimeIncrease = 10f; // Time increase when reaching a checkpoint

    bool gameOver = false;
    float timeLeft;

    // PROPERTIES
    public float CheckpointTimeIncrease => checkpointTimeIncrease;
    public float Timeleft // This is a property to access the time left in the game with other scripts.
    {
        get => timeLeft;
        set => timeLeft = value;
    }

    // METHODS
    public bool GameOver => gameOver; // Using expression-bodied property for simplicity. Same as above.

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }
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
        restartTextGO.SetActive(true);
        timeLeft = 0f;
        Time.timeScale = 0.1f;
        playerController.enabled = false;
    }
}