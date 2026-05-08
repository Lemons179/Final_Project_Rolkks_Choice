using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth2D : MonoBehaviour
{
    //audio 
    [SerializeField] private AudioClip playerHitSound;

    //audio 
    [SerializeField] private AudioClip playerDeathSound;

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

        //play hit sound
        soundFXManager.instance.PlaySoundFXClip(playerHitSound, transform, 1f);

        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            //play death sound
            soundFXManager.instance.PlaySoundFXClip(playerDeathSound, transform, 1f);
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