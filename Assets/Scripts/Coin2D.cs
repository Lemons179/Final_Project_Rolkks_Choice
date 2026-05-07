using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2D : MonoBehaviour
{
    //audio 
    [SerializeField] private AudioClip coinsound;

    public static int totalCoins = 0;
    
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
        if (other.CompareTag("Player") || other.CompareTag("player"))
        {
            //play coin sound
            SoundFXManager.instance.PlayCoinClip(coinsound, transform, 1f);
            totalCoins++;
            Destroy(gameObject);
        }
    }
}
