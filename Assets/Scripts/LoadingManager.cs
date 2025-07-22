using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;               // 🎯 Assign in Inspector
    public TextMeshProUGUI percentText;     // 🎯 Assign
    public TextMeshProUGUI loadingText;     // 🎯 Assign

    public float minLoadTime = 10f;         // ⏳ Minimum time before scene switches

    private float loadTimer = 0f;
    private int dotCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(UpdateDots), 0f, 0.5f);
        StartCoroutine(LoadMainMenu());
    }

    void UpdateDots()
    {
        dotCount = (dotCount + 1) % 4;
        if (loadingText != null)
            loadingText.text = "Loading" + new string('.', dotCount);
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("Main menu");
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            loadTimer += Time.deltaTime;

            float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(loadTimer / minLoadTime);
            float smoothProgress = Mathf.Min(progress, timeProgress);

            if (loadingBar != null)
                loadingBar.value = smoothProgress;

            if (percentText != null)
                percentText.text = Mathf.RoundToInt(smoothProgress * 100f) + "%";

            // When both scene is loaded and time has passed, activate scene
            if (progress >= 1f && loadTimer >= minLoadTime)
            {
                yield return new WaitForSeconds(0.5f); // optional smooth delay
                asyncOp.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
