using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Shared reference to the active GameManager in the scene.
    public static GameManager instance;

    // UI script responsible for showing the victory screen.
    [SerializeField] private VictoryUI victoryUI;
    [SerializeField] private GameOverUI gameOverUI;

    // Zone that becomes active after all coins are collected.
    [SerializeField] private GameObject victoryZone;
    // Door object shown while the victory zone is locked.
    [SerializeField] private GameObject closedDoor;

    // Tracks whether the win zone has already been activated.
    private bool victoryZoneActivated = false;
    
    // Tracks whether the player has already won.
    private bool playerHasWon = false;
    private bool isGameOver = false;
    void Awake()
    {
        // Store this GameManager as the active scene instance.
        instance = this;
        // Reset static coin counts for this level load.
        Coin2D.ResetCoinCounts();
    }

    void Start()
    {
        // Count all active coins in the level after scene objects have loaded.
        Coin2D.maxCoins = FindObjectsOfType<Coin2D>().Length;
        // Hide the win zone when the level begins.
        if (victoryZone != null)
        {
            victoryZone.SetActive(false);
        }

        // Show the closed door when the level begins.
        if (closedDoor != null)
        {
            closedDoor.SetActive(true);
        }
    }

    public void CheckCoinsCollected()
    {
        // Stop checking if the win zone has already been activated.
        if (victoryZoneActivated)
        {
            return;
        }

        // Activate the win zone once all coins have been collected.
        if (Coin2D.collectedCoins >= Coin2D.maxCoins)
        {
            ActivateVictoryZone();
        }
    }

    private void ActivateVictoryZone()
    {
        // Mark the win zone as activated.
        victoryZoneActivated = true;

        // Show or enable the win zone in the level.
        if (victoryZone != null)
        {
            victoryZone.SetActive(true);
        }

        // Hide the closed door after all coins are collected.
        if (closedDoor != null)
        {
            closedDoor.SetActive(false);
        }
    }

    public void WinGame()
    {
        // Prevent the win logic from running more than once.
        if (playerHasWon)
        {
            return;
        }

        // Mark the game as won.
        playerHasWon = true;

        // Show the victory screen.
        victoryUI.ShowVictory();
    }

    public void GameOver()
    {
        // Prevent the game over state from triggering more than once.
        if (isGameOver || playerHasWon)
        {
            return;
        }

        // Store that the game is over.
        isGameOver = true;

        // Show the game over UI.
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
        }
    }
}