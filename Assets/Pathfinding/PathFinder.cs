using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startingPt;
    public Vector2Int StartingPt { get { return startingPt; } }
    [SerializeField] Vector2Int endPt;
    public Vector2Int EndingPt { get { return endPt; } }

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
            startingNode = grid[startingPt];
            endNode = grid[endPt]; //Initializing both node variables.
            startingNode.isTreadable = true;
            endNode.isTreadable = true;
        }
    }

    void Start()
    {
        UpdatePath();
    }

    public List<Node> UpdatePath()
    {
        gridManager.ResetNode();
        BreadthFirstSearch(startingPt);
        return BuildPath();
    }

    public List<Node> UpdatePath(Vector2Int coordinates)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(coordinates);
        return BuildPath();
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
                neighbors[j].connectedTo = currentSearchNode; //Sets connectedTo variable in Node to currentSearchNode.
                explored.Add(neighbors[j].coordinates, neighbors[j]);
                frontier.Enqueue(neighbors[j]);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startingNode.isTreadable = true;
        endNode.isTreadable = true;

        frontier.Clear();
        explored.Clear();
        
        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]); //queue starting node.
        explored.Add(coordinates, grid[coordinates]);

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

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>(); //Initialize local list.
        Node currentNode = endNode;

        path.Add(currentNode); //Add endNode to path list.
        currentNode.isPath = true; //Sets isPath to true and color for nodes in isPath turns orange.

        while( currentNode.connectedTo != null) // Adds nodes connected to destination node to the path list and backtracks to start.
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates) //If it returns true, coordinates sent in are "blocked" and will not be able to place anything ontop.
    {
        bool previousState = grid[coordinates].isTreadable;

        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isTreadable = false;
            List<Node> newPath = UpdatePath();
            grid[coordinates].isTreadable = previousState; //Safeguard to switch isTreadable's state back to true, since previousState should always be set to true.

            if(newPath.Count <= 1) //Will recalculate the path if it does not reach past the first node.
            {
                UpdatePath();
                return true;
            }            
        }
        return false;
    }

    public void NotifyRecievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver); //String of function being broadcasted, and 2nd arguements allows to bypass requirements
                                                                                     //to broadcast and will send message to all scripts containing RecalculatePath anyway.
    }
}
