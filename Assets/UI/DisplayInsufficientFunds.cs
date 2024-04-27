using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInsufficientFunds : MonoBehaviour
{
    TextMeshProUGUI insufficientCurrency;
            
    void Start()
    {
        insufficientCurrency = GetComponent<TextMeshProUGUI>();
        insufficientCurrency.enabled = false; //Set disabled at start.
    }
    
    public void DisplayInsufficientGold(bool isDisplayed)
    {
        insufficientCurrency.enabled = isDisplayed;
    }
}
