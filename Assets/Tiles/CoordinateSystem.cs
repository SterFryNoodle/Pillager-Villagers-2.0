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
    
    TextMeshPro labelCoords;
    Vector2Int coordinates = new Vector2Int(); //Initialize variable w/ "new" keyword as it is a  normal
                                               //struct type and NOT a monobehavior class.
    EnemyDestination waypoint;

    void Awake()
    {
        labelCoords = GetComponent<TextMeshPro>();
        labelCoords.enabled = false;

        waypoint = GetComponentInParent<EnemyDestination>();
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
        if(waypoint.IsPlaceable)
        {
            labelCoords.color = defaultColor;
        }
        else
        {
            labelCoords.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {
        #if UNITY_EDITOR
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); //Rounds x-coord of object's parent to nearest int and stores it into x of coordinates variable.
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); //This grabs z-coord of object's parent instead of y on the tile coord system and stores
                                                                                                               //into the y of coordinates variable.
        #endif //Solves error and tells compiler that this code is meant for editor mode only.

        labelCoords.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString(); //Converts from Vector2 to string and stores into object's parent name.
    }
}
