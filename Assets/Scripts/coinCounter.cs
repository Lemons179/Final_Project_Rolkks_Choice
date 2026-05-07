using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coinCounter : MonoBehaviour
{
    //audio 
    //[SerializeField] private AudioClip coinsound;
    TMP_Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //play coin sound
        //SoundFXManager.instance.PlayCoinClip(coinsound, transform, 1f);
        //Set the current number of coins to display
        if (counterText.text != Coin2D.totalCoins.ToString())
        {
            counterText.text = Coin2D.totalCoins.ToString();
        }
    }
}