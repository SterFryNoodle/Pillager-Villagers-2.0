using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<EnemyDestination> path = new List<EnemyDestination>(); //Initialize variable type List.
    [SerializeField][Range(0f, 5f)] float enemySpeed = 1f;

    Enemy enemy;
    void OnEnable() //Resets the function everytime the gameObject attached is re-enabled.
    {
        FindPath();
        ReturnToBeginning();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear(); //ensures path does not build ontop of itself or repeat.

        GameObject destinationParent = GameObject.FindGameObjectWithTag("Path"); //Storing path parent gameObject w/ "path" tag

        foreach(Transform destinationChild in destinationParent.transform) //Finds the transform of each child object from the parent.
        {
            EnemyDestination enemyDestination = destinationChild.GetComponent<EnemyDestination>(); 

            if (enemyDestination != null) //Safeguard check for EnemyDestination component in each child object before adding into path list.
            {
                path.Add(enemyDestination); //Finding the EnemyDestination component in each object child under & adding into the list.
            }
        }
    }

    void ReturnToBeginning() //Sets enemies to first tile set in the path.
    {
        transform.position = path[0].transform.position;
    }

    void AtEndOfPath()
    {
        gameObject.SetActive(false);
        enemy.DeductGold();        
    }

    IEnumerator FollowPath() //Return type used with foreach loops when used in coroutines.
    {
        foreach(EnemyDestination destination in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = destination.transform.position;
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
