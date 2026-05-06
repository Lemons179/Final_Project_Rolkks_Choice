using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    // UI group containing the victory screen.
    [SerializeField] private CanvasGroup victoryPanel;

    // Button that returns to the main menu scene.
    [SerializeField] private Button startMenuButton;

    // Button that reloads the current scene.
    [SerializeField] private Button resetButton;

    // Name of the main menu scene.
    [SerializeField] private string startMenuSceneName = "StartMenu";

    void Start()
    {
        // Hide the victory panel when the scene starts.
        HideVictoryPanel();

        // Connect the main menu button if it exists.
        if (startMenuButton != null)
        {
            startMenuButton.onClick.AddListener(GoToStartMenu);
        }

        // Connect the reset button if it exists.
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(RestartGame);
        }
    }

    public void ShowVictory()
    {
        // Stop gameplay while the victory screen is visible.
        Time.timeScale = 0f;

        // Show the victory panel.
        if (victoryPanel != null)
        {
            victoryPanel.alpha = 1f;
            victoryPanel.interactable = true;
            victoryPanel.blocksRaycasts = true;
        }

        // Show the cursor for UI interaction.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideVictoryPanel()
    {
        // Hide the victory panel.
        if (victoryPanel != null)
        {
            victoryPanel.alpha = 0f;
            victoryPanel.interactable = false;
            victoryPanel.blocksRaycasts = false;
        }
    }

    private void GoToStartMenu()
    {
        // Resume normal time before changing scenes.
        Time.timeScale = 1f;

        // Load the main menu scene.
        SceneManager.LoadScene(startMenuSceneName);
    }

    private void RestartGame()
    {
        // Resume normal time before reloading the scene.
        Time.timeScale = 1f;

        // Reload the current gameplay scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}