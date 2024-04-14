using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Allows the class to serialize variables onto inspector.
public class Node
{
    public Vector2Int coordinates = new Vector2Int();
    public bool isTreadable;
    public bool isExplored;
    public bool isPath;

    public Node connectedTo;
        
    public Node(Vector2Int coords, bool isWalkable) //constructor
    {
        this.coordinates = coords; //Sets both arguement variable names to their respective initialized variables.
        this.isTreadable = isWalkable;
    }
}
