using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField] int startingBalance = 300;
    [SerializeField] TextMeshProUGUI currencyDisplay;
    [SerializeField] TextMeshProUGUI loseText;
        
    int currentBalance;
    float delayTime = 4f;
    
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
            DisplayLoseCondition();
            Invoke("ReloadScene" , delayTime);
        }        
    }

    void DisplayAvailableCurrency()
    {
        currencyDisplay.text = "Gold:" + currentBalance; //displays currency to UI.
    }

    void DisplayLoseCondition()
    {
        loseText.text = "Defeat";
    }
        
    void ReloadScene()
    {       
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);        
    }
}
