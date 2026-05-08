using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2D : MonoBehaviour
{
    //audio 
    [SerializeField] private AudioClip enemyHitSound;

    //audio 
    [SerializeField] private AudioClip enemydeathSound;

    public int health = 3;

    public void TakeDamage(int damage)
    {
        //play hit sound
        soundFXManager.instance.PlaySoundFXClip(enemyHitSound, transform, 1f);
        health -= damage;

        if (health <= 0)
        {
            //play death sound
            soundFXManager.instance.PlaySoundFXClip(enemydeathSound, transform, 1f);

            EnemyAI2D ai = GetComponent<EnemyAI2D>();
            if (ai != null)
                ai.Die();
            else
                Destroy(gameObject);
        }
    }
}
