using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] int goldCost = 50;

    CurrencySystem bank;
        
    public bool CreateTower(TowerPlacement tower, Vector3 position)
    {
        bank = FindObjectOfType<CurrencySystem>();
        
        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= goldCost)
        {
            bank.Withdraw(goldCost);
            Instantiate(tower.gameObject, position, Quaternion.identity);
            return true;
        }
        Debug.Log("Not enough gold to place tower.");
        return false;        
    }
}
