using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] TextMeshProUGUI currencyDisplay;

    int currentBalance;
    
    public int CurrentBalance { get { return currentBalance; } } //Creates a property of the private variable currentBalance.

    void Awake()
    {
        currentBalance = startingBalance;
        DisplayAvailableCurrency();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        DisplayAvailableCurrency();        
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        DisplayAvailableCurrency();

        if (currentBalance < 0)
        {
            ReloadScene();
        }        
    }

    void DisplayAvailableCurrency()
    {
        currencyDisplay.text = "Gold:" + currentBalance; //displays currency to UI.
    }

    void ReloadScene()
    {       
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);        
    }
}
