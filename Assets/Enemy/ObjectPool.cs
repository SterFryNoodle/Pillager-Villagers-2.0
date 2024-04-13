using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField][Range(0.1f, 15f)] float spawnTimer = 1f;
    [SerializeField][Range(0, 50)] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();

            if (spawnTimer > 0f)
            {
                yield return new WaitForSeconds(spawnTimer);
            }            
        }
    }
     void EnableObjectInPool()
    {
        for (int j = 0; j < pool.Length; j++)
        {
            if (pool[j].activeInHierarchy == false)
            {
                pool[j].SetActive(true);
                return; //resets the loop early instead of waiting for enemy object to reach end of path to spawn another.
            }            
        }
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize]; //Initializes pool array with size.

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false); //Sets gameObjects within array as disabled by default.
        }
    }    
}
