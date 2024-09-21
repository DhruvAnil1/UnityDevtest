using UnityEngine;
using UnityEngine.UI;

public class CollectCube : MonoBehaviour
{
    public int coins = 0;
    public Text scoreText;
    public GameObject winPanel;

    private int totalCoins;

    private void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);

        totalCoins = GameObject.FindGameObjectsWithTag("Cube").Length;
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Cube"))
        {
            coins++;
            Destroy(col.gameObject);
            UpdateScoreText();

            if (coins >= totalCoins)
                ShowWinPanel();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {coins}";
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
    }
}
