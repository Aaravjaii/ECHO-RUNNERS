using UnityEngine;
using TMPro;
using System.Collections;

public class ComboUI : MonoBehaviour
{
    public TextMeshProUGUI comboText;
    public ScoreManager scoreManager;

    private int lastMultiplier = 1;

    void Update()
    {
        comboText.text = "Combo: x" + scoreManager.multiplier;

        if (scoreManager.multiplier > lastMultiplier)
        {
            StopAllCoroutines();  // In case combo rises multiple times quickly
            StartCoroutine(GrowText());
            lastMultiplier = scoreManager.multiplier;
        }
    }

    IEnumerator GrowText()
    {
        comboText.fontSize = 60;  // Make it bigger
        yield return new WaitForSeconds(0.2f);
        comboText.fontSize = 36;  // Back to normal
    }
}
