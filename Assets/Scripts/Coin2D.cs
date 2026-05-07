using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2D : MonoBehaviour
{
    //audio 
    [SerializeField] private AudioClip coinsound;

    public static int collectedCoins = 0;
    public static int maxCoins = 0;

    void Awake()
    {
        Collider2D coinCollider = GetComponent<CircleCollider2D>();
        if (coinCollider != null)
        {
            coinCollider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //play coin sound
            soundFXManager.instance.PlaySoundFXClip(coinsound, transform, 1f);


            // Increase the collected coin count.
            collectedCoins++;

            // Check whether all coins have been collected.
            if (GameManager.instance != null)
            {
                GameManager.instance.CheckCoinsCollected();
            }

            // Remove the collected coin from the scene.
            Destroy(gameObject);
        }
    }

    public static void ResetCoinCounts()
    {
        // Reset coin counts before the level coins count themselves.
        collectedCoins = 0;
        maxCoins = 0;
    }
}
