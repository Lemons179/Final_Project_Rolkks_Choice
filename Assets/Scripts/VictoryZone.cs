using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check whether the player entered the victory zone.
        if (other.CompareTag("Player"))
        {
            // Trigger the game's victory state.
            GameManager.instance.WinGame();
        }
    }
}