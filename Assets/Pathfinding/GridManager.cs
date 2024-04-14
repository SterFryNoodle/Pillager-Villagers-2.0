using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); //Initializing a dictionary w/ Vector2Int type key & Node type value.

    [SerializeField] Vector2Int gridSize;

    void Awake()
    {
        CreateGrid();

    }

    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++) //This nested loop will iterate through a grid by every column starting @ 0,0.
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y); //initializing a key holding the coordinates being iterated.
                grid.Add(coordinates, new Node(coordinates, true));
                Debug.Log(grid[coordinates].coordinates + " is " + grid[coordinates].isTreadable); //Sending the dictionary variable & passing in the key to
                                                                                                   //access the node script.
            }
        }
    }
}
