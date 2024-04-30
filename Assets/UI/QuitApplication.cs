using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
        
    void Update()
    {
        QuitOnEsc();
    }
     void QuitOnEsc()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }
}
