using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coinCounter : MonoBehaviour
{
    TMP_Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TMP_Text>();
        // Display the starting coin count.
        UpdateCounterText();
    }

    // Update is called once per frame
    void Update()
    {
        // Refresh the displayed coin count.
        UpdateCounterText();
    }
    void UpdateCounterText()
    {
        // Display collected coins out of total coins.
        counterText.text = Coin2D.collectedCoins + " / " + Coin2D.maxCoins;
    }
}