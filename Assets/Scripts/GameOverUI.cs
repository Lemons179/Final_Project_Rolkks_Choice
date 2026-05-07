using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // UI group containing the game over screen.
    [SerializeField] private CanvasGroup gameOverPanel;

    // Button that returns to the start menu scene.
    [SerializeField] private Button startMenuButton;

    // Button that reloads the current scene.
    [SerializeField] private Button resetButton;

    // Name of the start menu scene.
    [SerializeField] private string startMenuSceneName = "StartMenu";

    // Player shooting script disabled when the game over screen is shown.
    [SerializeField] private PlayerShoot2D playerShoot;

    void Start()
    {
        // Hide the game over panel when the scene starts.
        HideGameOverPanel();

        // Connect the start menu button if it exists.
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

    public void ShowGameOver()
    {
        // Stop gameplay while the game over screen is visible.
        Time.timeScale = 0f;

        // Show the game over panel.
        if (gameOverPanel != null)
        {
            gameOverPanel.alpha = 1f;
            gameOverPanel.interactable = true;
            gameOverPanel.blocksRaycasts = true;
        }

        // Show the cursor for UI interaction.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable player shooting during the game over state.
        if (playerShoot != null)
        {
            playerShoot.enabled = false;
        }
    }

    private void HideGameOverPanel()
    {
        // Hide the game over panel.
        if (gameOverPanel != null)
        {
            gameOverPanel.alpha = 0f;
            gameOverPanel.interactable = false;
            gameOverPanel.blocksRaycasts = false;
        }
    }

    private void GoToStartMenu()
    {
        // Resume normal time before changing scenes.
        Time.timeScale = 1f;

        // Load the start menu scene.
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