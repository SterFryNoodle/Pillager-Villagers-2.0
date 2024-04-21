using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] TowerPlacement towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } //This property of the bool variable allows other scripts to access the variable
                                                            //w/o changing access of the variable itself and allowing change to anything else in this script. 
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position); 
            
            isPlaceable = !isPlaced;
        }
    }        
}
