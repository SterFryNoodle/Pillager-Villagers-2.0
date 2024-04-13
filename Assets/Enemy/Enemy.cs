using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int killReward = 25;
    [SerializeField] int goldPenalty = 25;

    CurrencySystem bank;
    void Start()
    {
        bank = FindObjectOfType<CurrencySystem>();
    }
    
    public void RewardGold() //Make return type bool later to create a safeguard for successful or failed deposits.
    {
        if (bank == null)
        {
            return;
        }

        bank.Deposit(killReward); //Deposits gold into currencySystem script function.
    }

    public void DeductGold() //Make return type bool later to create a safeguard for successful or failed withdraws.
    {
        if (bank == null)
        {
            return;
        }

        bank.Withdraw(goldPenalty); //Withdraws gold from currencySystem script function.
    }
}
