using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Properties;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("Should match Editor increment snap settings")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get {  return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); //Initializing a dictionary w/ Vector2Int type key & Node type value.
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }


    void Awake()
    {
        CreateGrid();

    }

    public Node GetNode(Vector2Int coordinates) //Sends in the key as an arguement and returns value of a specific node.
    {
        if(grid.ContainsKey(coordinates)) // Check grid to see if it contains key needed stored in coordinates variable.
        {
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isTreadable = false;
        }
    }

    public Vector2Int GetCoordsFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize); //Rounds x-coord of object's parent to nearest int and stores it into x of coordinates variable.
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize); //This grabs z-coord of object's parent instead of y on the tile coord system and stores
                                                                                                               //into the y of coordinates variable.
        return coordinates;
    }

    public Vector3 GetPositionFromCoords(Vector2Int coords)
    {
        Vector3 position = new Vector3();

        position.x =Mathf.RoundToInt(coords.x * unityGridSize);
        position.z =Mathf.RoundToInt(coords.y * unityGridSize);

        return position;
    }
    void CreateGrid()
    {
        for(int x = 0; x <= gridSize.x; x++) //This nested loop will iterate through a grid by every column starting @ 0,0.
        {
            for (int y = 0; y <= gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y); //Initializing a key holding the coordinates being iterated from grid.
                grid.Add(coordinates, new Node(coordinates, true)); //Pairs each key with the value "true" default value and adds it to the grid.                
            }
        }
    }
}
