using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles worldposition, walkable areas
/// </summary>
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
    /// 
    /// </summary>
    public int gridX;
    /// <summary>
    /// 
    /// </summary>
    public int gridY;

    /// <summary>
    /// 
    /// </summary>
    public int gCost;

    /// <summary>
    /// 
    /// </summary>
    public int hCost;

    /// <summary>
    /// 
    /// </summary>
    public Node parent;

    /// <summary>
    /// constructor of the class
    /// </summary>
    /// <param name="_walkable">Walkable area</param>
    /// <param name="_worldPos"> world co ordinates</param>
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    /// <summary>
    /// Sum of the value of gCost and hcost
    /// </summary>
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
