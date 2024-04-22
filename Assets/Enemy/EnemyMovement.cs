using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{    
    [SerializeField][Range(0f, 5f)] float enemySpeed = 1f;

    List<Node> path = new List<Node>(); //Initialize variable type List.
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;
    void OnEnable() //Resets the function everytime the gameObject attached is re-enabled.
    {
        FindPath();
        ReturnToBeginning();
        StartCoroutine(FollowPath());
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void FindPath()
    {
        path.Clear(); //ensures path does not build ontop of itself or repeat.

        path = pathFinder.UpdatePath();
    }

    void ReturnToBeginning() //Sets enemies to first tile set in the path.
    {
        transform.position = gridManager.GetPositionFromCoords(pathFinder.StartingPt);
    }

    void AtEndOfPath()
    {
        gameObject.SetActive(false);
        enemy.DeductGold();        
    }

    IEnumerator FollowPath() //Return type used with foreach loops when used in coroutines.
    {
        for(int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoords(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition); // rotates object based on forward movement of wherever the next destination is.
            
            while(travelPercent <= 1f)
            {
                travelPercent += Time.deltaTime * enemySpeed; // increases variable based on frame time and speed variable.
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); // sets current object position to a percentage to the way between start and end positions.

                yield return new WaitForEndOfFrame();
            }            
        }      
        AtEndOfPath();
    }
}
