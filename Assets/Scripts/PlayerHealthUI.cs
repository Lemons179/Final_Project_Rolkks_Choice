using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    // Text component used to display the player's health.
    private TMP_Text healthText;

    // Player health component used as the source of the health value.
    [SerializeField] private PlayerHealth2D playerHealth;

    void Start()
    {
        // Get the TextMeshPro component attached to this UI object.
        healthText = GetComponent<TMP_Text>();

        // Display the starting health value.
        UpdateHealthText();
    }

    void Update()
    {
        // Refresh the displayed health value.
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        // Stop if required references are missing.
        if (healthText == null || playerHealth == null)
        {
            return;
        }

        // Display current health out of maximum health.
        healthText.text = "Health: " + playerHealth.CurrentHealth + " / " + playerHealth.maxHealth;
    }
}