using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public AudioSource gameOverMusic;        // 🎵 Drag your Game Over AudioSource here
    public BeatManager beatManager;          // 🎧 Drag BeatManager object here in Inspector
    public GameObject gameOverPanel;         // 🎮 Assign GameOverPanel UI
    public TextMeshProUGUI finalScoreText;   // 📝 Assign Final Score Text UI
    public ScoreManager scoreManager;        // 📊 Drag ScoreManager here

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide Game Over UI at start
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);  // Show Game Over panel

        // 🔒 Disable Player Movement + Trigger Death Animation
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.enabled = false;
            }

            // 🎭 Trigger death animation (supports both Animation and Animator)
            Animator animator = player.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("die");
            }
            else
            {
                Animation legacyAnim = player.GetComponent<Animation>();
                if (legacyAnim != null && legacyAnim.GetClip("die") != null)
                {
                    legacyAnim.Play("die");
                }
            }

            // 🧊 Freeze player in place
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }

        // 📝 Show Final Score
        if (finalScoreText != null && scoreManager != null)
        {
            finalScoreText.text = "Final Score: " + scoreManager.score.ToString();
        }

        // 🎵 Stop BeatManager background music
        if (beatManager != null)
        {
            beatManager.StopMusic();  // ✅ You must define StopMusic() in BeatManager
        }

        // ▶️ Play Game Over Music
        if (gameOverMusic != null && !gameOverMusic.isPlaying)
        {
            gameOverMusic.Play();
        }

        // 🧊 Freeze scene completely
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
