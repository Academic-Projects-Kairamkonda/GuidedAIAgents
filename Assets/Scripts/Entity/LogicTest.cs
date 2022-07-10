using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTest : MonoBehaviour
{
    private bool run=true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!run)
            {
                Debug.Log($"Run fast");
                run = true;
            }

        }
    }
}
