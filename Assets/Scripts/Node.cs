using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    /// <summary>
    /// Walkable area
    /// </summary>
    public bool walkable;

    /// <summary>
    /// Position of nodes
    /// </summary>
    public Vector3 worldPosition;


    /// <summary>
    /// constructor of the class
    /// </summary>
    /// <param name="_walkable">Walkable area</param>
    /// <param name="_worldPos"> world co ordinates</param>
    public Node(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }
}
