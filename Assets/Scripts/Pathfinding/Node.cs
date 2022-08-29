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
    public bool walkableRegion;

    /// <summary>
    /// Position of nodes
    /// </summary>
    public Vector3 worldLocation;

    /// <summary>
    /// position of gridX
    /// </summary>
    public int gridofX;

    /// <summary>
    /// position of gridY
    /// </summary>
    public int gridofY;

    /// <summary>
    /// 
    /// </summary>
    public int movementPenalty;

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
    /// <param name="_walkableRegion">Walkable area</param>
    /// <param name="_worldLocation"> world co ordinates</param>
    public Node(bool _walkableRegion, Vector3 _worldLocation, int _gridofX, int _gridofY, int _defaultpenalty)
    {
        walkableRegion = _walkableRegion;
        worldLocation = _worldLocation;
        gridofX = _gridofX;
        gridofY = _gridofY;
        movementPenalty = _defaultpenalty;
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
    /// <param name="compareNode"></param>
    /// <returns></returns>
    public int CompareTo(Node compareNode)
    {
        int compareValue = fCost.CompareTo(compareNode.fCost);
        if(compareValue==0)
        {
            compareValue = hCost.CompareTo(compareNode.hCost);
        }
        return -compareValue;
    }
}
