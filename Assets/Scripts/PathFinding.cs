using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


/// <summary>
/// Calculates the path form seeker to target
/// </summary>
public class PathFinding : MonoBehaviour
{
    /// <summary>
    /// Grid plane positions
    /// </summary>
    GridPlane gridPlane;

    /// <summary>
    /// Seekes target for path
    /// </summary>
    public Transform seeker;

    /// <summary>
    /// target for seeker to calculate path
    /// </summary>
    public Transform target;

    #region Unity Methods

    void Awake()
    {
        gridPlane = GetComponent<GridPlane>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        FindPath(seeker.position, target.position);
    }

    #endregion Unity Methods

    /// <summary>
    /// Calculates the route to target position
    /// </summary>
    /// <param name="startPos">start position</param>
    /// <param name="targetPos">end position</param>
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Node startNode = gridPlane.NodeFromWorldPoint(startPos);
        Node targetNode = gridPlane.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(gridPlane.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count>0)
        {
            /* This function derived 
            Node currentNode = openSet[0];
           
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost<currentNode.fCost    ||
                    openSet[i].fCost==currentNode.fCost &&
                    openSet[i].hCost <currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            */

            Node currentNode = openSet.RemoveFirst();

            closedSet.Add(currentNode);

            if (currentNode==targetNode)
            {
                sw.Stop();
                print($"Path Found: {sw.ElapsedMilliseconds} ms");
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neigbour in gridPlane.GetNeighbours(currentNode))
            {
                if (!neigbour.walkable || closedSet.Contains(neigbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neigbour);

                if(newMovementCostToNeighbour<neigbour.gCost || !openSet.Contains(neigbour))
                {
                    neigbour.gCost = newMovementCostToNeighbour;
                    neigbour.hCost = GetDistance(neigbour, targetNode);
                    neigbour.parent = currentNode;

                    if (!openSet.Contains(neigbour))
                    {
                        openSet.Add(neigbour);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Add the path to the parent 
    /// </summary>
    /// <param name="startNode">start position</param>
    /// <param name="endNode">end position</param>
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode!= startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        gridPlane.path = path;
    }

    /// <summary>
    /// Gets the distance from one to another point
    /// </summary>
    /// <param name="nodeA">seeker</param>
    /// <param name="nodeB">target</param>
    /// <returns></returns>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX>distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distY + 10 * (distY - distX);
    }
}
