using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateSystem : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, .5f, 0f); //Creates a new color that isn't the predetermined primary colors.

    TextMeshPro labelCoords;
    Vector2Int coordinates = new Vector2Int(); //Initialize variable w/ "new" keyword as it is a normal
                                               //struct type and NOT a monobehavior class.
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        labelCoords = GetComponent<TextMeshPro>();
        labelCoords.enabled = false;
        
        DisplayCoordinates(); //Displays current coords text once in play mode; Doesn't update.
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates(); //Updates & displays the coords while NOT in play mode.
            UpdateObjectName(); 
        }

        SetCoordinateColor(); //Visually shows which tiles are placeable and not placeable.
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            labelCoords.enabled = !labelCoords.IsActive();
        }
    }

    void SetCoordinateColor()
    {
        if (gridManager == null) //If gridmanager is not found, return early.
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates); //Set local variable to coords stored in GetNode().

        if (node == null) //If node is not found, return early.
        {
            return;
        }

        if(!node.isTreadable)
        {
            labelCoords.color = blockedColor;
        }
        else if(node.isPath)
        {
            labelCoords.color = pathColor;
        }
        else if(node.isExplored)
        {
            labelCoords.color = exploredColor;
        }
        else
        {
            labelCoords.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        if(gridManager == null)
        { 
            return; 
        }
        
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize); //Rounds x-coord of object's parent to nearest int and stores it into x of coordinates variable.
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize); //This grabs z-coord of object's parent instead of y on the tile coord system and stores
                                                                                                   //into the y of coordinates variable.        
        labelCoords.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString(); //Converts from Vector2 to string and stores into object's parent name.
    }
}
