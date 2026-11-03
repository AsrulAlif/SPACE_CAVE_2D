
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pauseMenuPanel;

    [Header("Main Menu")]
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    // Mengakses timer Player tanpa mengubah Player.cs
    private void ResetPlayerTimer()
    {
        System.Type playerType = typeof(Player);

        FieldInfo elapsedTimeField = playerType.GetField(
            "elapsedTime",
            BindingFlags.Static | BindingFlags.NonPublic
        );

        FieldInfo timerRunningField = playerType.GetField(
            "timerIsRunning",
            BindingFlags.Static | BindingFlags.NonPublic
        );

        if (elapsedTimeField != null)
        {
            elapsedTimeField.SetValue(null, 0f);
        }
        else
        {
            Debug.LogError("Field elapsedTime tidak ditemukan!");
        }

        if (timerRunningField != null)
        {
            timerRunningField.SetValue(null, true);
        }
        else
        {
            Debug.LogError("Field timerIsRunning tidak ditemukan!");
        }
    }

    void Start()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenuPanel == null)
            return;

        pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel == null)
            return;

        pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;

        ResetPlayerTimer();

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;

        ResetPlayerTimer();

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
