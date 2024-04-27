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
    PathFinder pathFinder;
    DisplayInsufficientFunds display;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordsFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isTreadable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isInstantiated = towerPrefab.CreateTower(towerPrefab, transform.position);            
            
            if(isInstantiated)
            {
                gridManager.BlockNode(coordinates); //send coords of tile that tower was instantiated ontop of to set isTreadable to true.
                pathFinder.NotifyRecievers();
            }
            else
            {
                display.DisplayInsufficientGold(true);
            }
        }
    }        
}
