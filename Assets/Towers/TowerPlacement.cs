using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] int goldCost = 50;
    [SerializeField] int buildTimer = 2;
    [SerializeField] AudioClip towerConstruction;
    
    CurrencySystem bank;
    AudioSource buildingSource;
    
    void Start()
    {
        buildingSource = GetComponent<AudioSource>();
        StartCoroutine(BuildTower());        
    }
    
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
        return false;
    }
    
    IEnumerator BuildTower()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);                
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            BuildingTowerSFX();
            yield return new WaitForSeconds(buildTimer);    
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);                
            }
        }
    }

    void BuildingTowerSFX()
    {
        if(!buildingSource.isPlaying)
        {
            buildingSource.PlayOneShot(towerConstruction);
        }
    }
}
