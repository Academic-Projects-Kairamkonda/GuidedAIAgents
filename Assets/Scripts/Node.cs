using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles worldposition, walkable areas
/// </summary>
public class Node: IHeapItem<Node>
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
    /// position of gridX
    /// </summary>
    public int gridX;

    /// <summary>
    /// position of gridY
    /// </summary>
    public int gridY;

    /// <summary>
    /// position of one diagonal move
    /// </summary>
    public int gCost;

    /// <summary>
    /// position of two diagonal move
    /// </summary>
    public int hCost;

    /// <summary>
    /// parent node holds data of grid x and y position
    /// </summary>
    public Node parent;

    /// <summary>
    /// heap index of each node
    /// </summary>
    int heapIndex;

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

    /// <summary>
    /// property, return value of the heapIndex
    /// </summary>
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    /// <summary>
    /// Compare the nodes from fcost and hcost
    /// </summary>
    /// <param name="nodeToCompare"></param>
    /// <returns></returns>
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare==0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
