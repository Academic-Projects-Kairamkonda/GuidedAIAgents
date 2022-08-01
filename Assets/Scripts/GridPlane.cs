using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the grid generation with nodes
/// </summary>
public class GridPlane : MonoBehaviour
{
    /* Update the player position on the grid
      public Transform player;
     */

    /// <summary>
    /// LayerMask for unwalkable area
    /// </summary>
    public LayerMask unwalkableMask;

    /// <summary>
    /// World size of player
    /// </summary>
    public Vector2 gridWorldSize;

    /// <summary>
    /// Node radius of each gird;s
    /// </summary>
    public float nodeRadius;

    /// <summary>
    /// Grid node holds data of a current grid
    /// </summary>
    Node[,] grid;

    /// <summary>
    /// Diameter of the node
    /// </summary>
    float nodeDiameter;

    /// <summary>
    /// grid of the plane x axis
    /// </summary>
    int gridSizeX;

    /// <summary>
    /// grid of the plane y axis
    /// </summary>
    int  gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    /// <summary>
    /// Creates the grid of world point co ordinates
    /// </summary>
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y *nodeDiameter+ nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }
    }

    /// <summary>
    /// Neighbours data
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1 ; x <= 1; x++)
        {
            for (int y = -1; y <=1; y++)
            {
                if (x==0 && y==0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX>=0 && checkX<gridSizeX && checkY>=0 && checkY<gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Get the currrent node coordinates of the target object
    /// </summary>
    /// <param name="worldPosition">target position</param>
    /// <returns></returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    /// <summary>
    /// path calculates from seeker to target
    /// </summary>
    public List<Node> path;
   
    /// <summary>
    /// Draws Runtime nodes of the grid
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid!=null)
        {
            //Node playerNode = NodeFromWorldPoint(player.position);

            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;

                /*Checking the player current position on the Node
                if(playerNode==n)
                    Gizmos.color = Color.cyan;
                */

                if (path!=null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
