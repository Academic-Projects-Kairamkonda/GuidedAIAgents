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

    public int gridX;
    public int gridY;

    public int gCost;

    public int hCost;

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

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
