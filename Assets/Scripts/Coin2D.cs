using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2D : MonoBehaviour
{
    public static int collectedCoins = 0;
    public static int maxCoins = 0;

    void Awake()
    {
        // Count this coin as part of the level's total coin count.
        maxCoins++;
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
            // Increase the collected coin count.
            collectedCoins++;
            // Remove the collected coin from the scene.
            Destroy(gameObject);
        }
    }
}
