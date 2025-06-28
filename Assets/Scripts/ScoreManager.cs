using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int combo = 0;
    public int multiplier = 1;

    public void AddScore(int baseScore)
    {
        score += baseScore * multiplier;
        Debug.Log("Score: " + score);

        combo++;
        if (combo % 5 == 0)  // Example: every 5 dodges, increase multiplier
        {
            multiplier++;
            Debug.Log("Multiplier increased! x" + multiplier);
        }
    }

    public void ResetCombo()
    {
        combo = 0;
        multiplier = 1;
        Debug.Log("Combo reset. Multiplier back to x1");
    }
}
