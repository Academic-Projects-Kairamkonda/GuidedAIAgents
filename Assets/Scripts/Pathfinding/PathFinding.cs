using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;


/// <summary>
/// Calculates the path form seeker to target
/// </summary>
public class PathFinding : MonoBehaviour
{
    /// <summary>
    /// Grid plane positions
    /// </summary>
    GridPlane gridPlane;

    #region Unity Methods

    void Awake()
    {
        gridPlane = GetComponent<GridPlane>();
    }

    #endregion Unity Methods

    /// <summary>
    /// Calculates the route to target position
    /// </summary>
    /// <param name="startPos">start position</param>
    /// <param name="targetPos">end position</param>
   public void GenerateFindPath(EntityRequestPath request, Action<PathResult> callback)
    {
        Vector3[] pointsInWay = new Vector3[0];
        bool pathSucess = false;

        Node initialNode = gridPlane.NodeFromWorldLocation(request.initialPath);
        Node targetedNode = gridPlane.NodeFromWorldLocation(request.finalPath);

        if (initialNode.walkableRegion && targetedNode.walkableRegion)
        {

            Heap<Node> openSet = new Heap<Node>(gridPlane.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(initialNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();

                closedSet.Add(currentNode);

                if (currentNode == targetedNode)
                {
                    pathSucess = true;
                    break;
                }

                foreach (Node neighbour in gridPlane.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkableRegion || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour)+neighbour.movementPenalty;

                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetedNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
						}
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }


        if (pathSucess)
        {
            pointsInWay = RetracePath(initialNode, targetedNode);
            pathSucess = pointsInWay.Length > 0;
        }

        callback(new PathResult(pointsInWay, pathSucess, request.callback));
    }

    /// <summary>
    /// Add the path to the parent 
    /// </summary>
    /// <param name="startNode">start position</param>
    /// <param name="endNode">end position</param>
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode!= startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;

       // gridPlane.path = path;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridofX - path[i].gridofX, path[i - 1].gridofY - path[i].gridofY);
            if (directionNew!= directionOld)
            {
                waypoints.Add(path[i].worldLocation);
            }
            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }

    /// <summary>
    /// Gets the distance from one to another point
    /// </summary>
    /// <param name="nodeA">seeker</param>
    /// <param name="nodeB">target</param>
    /// <returns></returns>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridofX - nodeB.gridofX);
        int distY = Mathf.Abs(nodeA.gridofY - nodeB.gridofY);

        if (distX>distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distX + 10 * (distY - distX);
    }
}
