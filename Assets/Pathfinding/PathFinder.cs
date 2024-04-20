using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startingPt;
    [SerializeField] Vector2Int endPt;
    Node startingNode;
    Node endNode;
    Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    Node currentSearchNode;
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};    
    
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null )
        {
            grid = gridManager.Grid; //Accesses the dictionary initialized in GridManager.
        }        
    }

    void Start()
    {
        startingNode = gridManager.Grid[startingPt];
        endNode = gridManager.Grid[endPt]; //Initializing both node variables.
        BreadthFirstSearch();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + directions[i]; //coordinates stored in Node script added to current direction iteration
            
            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);                                
            }
        }

        for(int j = 0; j < neighbors.Count; j++)
        {
            if (!explored.ContainsKey(neighbors[j].coordinates) && neighbors[j].isTreadable)
            {
                explored.Add(neighbors[j].coordinates, neighbors[j]);
                frontier.Enqueue(neighbors[j]);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startingNode);
        explored.Add(startingPt, startingNode);

        while( frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue(); //Sets currentSearchNode to first node in queue.
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == endPt)
            {
                isRunning = false;
            }
        }
    }
}
