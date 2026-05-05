using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth2D : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

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
            Debug.Log("Player died");
            gameObject.SetActive(false);
        }
    }
}
