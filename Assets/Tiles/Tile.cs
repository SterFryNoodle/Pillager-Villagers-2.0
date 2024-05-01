using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] TowerPlacement towerPrefab;
    [SerializeField] TowerPlacement towerPrefabTwo;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } //This property of the bool variable allows other scripts to access the variable
                                                            //w/o changing access of the variable itself and allowing change to anything else in this script. 
    GridManager gridManager;
    PathFinder pathFinder;
    DisplayInsufficientFunds display;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        display = FindObjectOfType<DisplayInsufficientFunds>();
                
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordsFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
        
    void OnMouseOver()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (gridManager.GetNode(coordinates).isTreadable && !pathFinder.WillBlockPath(coordinates))
            {
                bool isInstantiated = towerPrefab.CreateTower(towerPrefab, transform.position);

                if (isInstantiated)
                {
                    gridManager.BlockNode(coordinates); //send coords of tile that tower was instantiated ontop of to set isTreadable to true.
                    pathFinder.NotifyRecievers();
                    display.DisplayInsufficientGold(false);
                }
                else
                {
                    display.DisplayInsufficientGold(true);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (gridManager.GetNode(coordinates).isTreadable && !pathFinder.WillBlockPath(coordinates))
            {
                bool isInstantiated = towerPrefab.CreateTower(towerPrefabTwo, transform.position);

                if (isInstantiated)
                {
                    gridManager.BlockNode(coordinates); //send coords of tile that tower was instantiated ontop of to set isTreadable to true.
                    pathFinder.NotifyRecievers();
                    display.DisplayInsufficientGold(false);
                }
                else
                {
                    display.DisplayInsufficientGold(true);
                }
            }
        }
        
    }    
}
