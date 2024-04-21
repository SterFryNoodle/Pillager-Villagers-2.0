using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); //Initializing a dictionary w/ Vector2Int type key & Node type value.
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }


    void Awake()
    {
        CreateGrid();

    }

    public Node GetNode(Vector2Int coordinates) //Sends in the key as an arguement and returns value of a specific node.
    {
        if(grid.ContainsKey(coordinates)) // Check grid to see if it contains key needed.
        {
            return grid[coordinates];
        }
        return null;
    }

    void CreateGrid()
    {
        for(int x = 0; x <= gridSize.x; x++) //This nested loop will iterate through a grid by every column starting @ 0,0.
        {
            for (int y = 0; y <= gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y); //Initializing a key holding the coordinates being iterated.
                grid.Add(coordinates, new Node(coordinates, true)); //Pairs each key with the value "true" and adds it to the grid.                
            }
        }
    }
}
