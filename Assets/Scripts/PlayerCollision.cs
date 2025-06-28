using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameOverManager gameOverManager;
    public ScoreManager scoreManager;  // ✅ Added line

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Player hit an obstacle.");
            scoreManager.ResetCombo();  // ✅ Added line
            gameOverManager.ShowGameOver();
        }
    }
}
