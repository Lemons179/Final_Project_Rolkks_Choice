using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth2D : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    
    // Current player health value exposed for UI display.
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");

        // Trigger the game over state.
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }

    }
}