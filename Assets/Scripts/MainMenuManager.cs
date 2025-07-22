using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingOverlay;        // 🎯 Drag the Loading Panel here
    public Slider loadingBar;                // 🎯 Optional
    public TextMeshProUGUI percentText;      // 🎯 Optional

    public void StartGame()
    {
        loadingOverlay.SetActive(true);      // Show loading UI
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("SampleScene");
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);

            if (loadingBar != null)
                loadingBar.value = progress;

            if (percentText != null)
                percentText.text = Mathf.RoundToInt(progress * 100f) + "%";

            if (asyncOp.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); // Optional smooth delay
                asyncOp.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
