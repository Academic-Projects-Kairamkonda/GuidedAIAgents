using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GridPlane gridPlane;

    public Transform seeker;
    public Transform target;

    void Awake()
    {
        gridPlane = GetComponent<GridPlane>();
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }


    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = gridPlane.NodeFromWorldPoint(startPos);
        Node targetNode = gridPlane.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count>0)
        {
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
            closedSet.Add(currentNode);

            if (currentNode==targetNode)
            {
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

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeB.gridY - nodeB.gridY);

        if (distX>distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distY + 10 * (distY - distX);
    }

        
}
