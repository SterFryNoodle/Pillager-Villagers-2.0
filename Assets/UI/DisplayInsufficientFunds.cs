using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInsufficientFunds : MonoBehaviour
{
    TextMeshProUGUI insufficientCurrency;
    int textTimer;

    void Start()
    {
        insufficientCurrency = GetComponent<TextMeshProUGUI>();
        insufficientCurrency.enabled = !insufficientCurrency.IsActive();
    }

    public void DisplayInsufficientGold(bool isShowing)
    {
        insufficientCurrency.enabled = isShowing;
        insufficientCurrency.text = "Not enough gold to place tower."; 
        
        while (textTimer < 3)
        {
            textTimer++;
        }
        insufficientCurrency.enabled = !isShowing;
    }
}
