using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 10f;
    public Text countdownText;
    public GameObject gameOverPanel;

    private float timeRemaining;
    private bool isGameOver = false;

    private void Start()
    {
        timeRemaining = startTime;
        UpdateCountdownText();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownText();
            }
            else
            {
                timeRemaining = 0;
                UpdateCountdownText();
                ShowGameOverPanel();
                isGameOver = true; // Ensure the game does not pause multiple times
            }
        }
    }

    private void UpdateCountdownText()
    {
        if (countdownText != null)
            countdownText.text = timeRemaining.ToString("F2");
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
    }
}
