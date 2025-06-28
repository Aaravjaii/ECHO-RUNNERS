using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // ✅ Required for TextMeshProUGUI

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText; // ✅ Drag FinalScoreText TMP UI object here
    public ScoreManager scoreManager;      // ✅ Drag ScoreManager object here

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide panel at the start
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);  // Show Game Over UI

        // Disable player movement instead of freezing time
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.enabled = false;
            }
        }

        // ✅ Show final score
        if (finalScoreText != null && scoreManager != null)
        {
            finalScoreText.text = "Final Score: " + scoreManager.score.ToString();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Unpause the game (just in case)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}
