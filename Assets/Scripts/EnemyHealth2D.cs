using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2D : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EnemyAI2D ai = GetComponent<EnemyAI2D>();
            if (ai != null)
                ai.Die();
            else
                Destroy(gameObject);
        }
    }
}
