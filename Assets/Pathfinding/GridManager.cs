using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Node node;
    
    void Start()
    {
        Debug.Log(node.coordinates);
        Debug.Log(node.isTreadable);
    }

    
    void Update()
    {
        
    }
}
