using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the grid generation with nodes
/// </summary>
public class GridPlane : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public bool displayGridGizmos;

    /// <summary>
    /// LayerMask for unwalkable area
    /// </summary>
    public LayerMask unwalkableMask;

    /// <summary>
    /// World size of player
    /// </summary>
    public Vector2 gridWorldCoordinateSize;

    /// <summary>
    /// Node radius of each gird;s
    /// </summary>
    public float radiusOfNode;

    /// <summary>
    /// 
    /// </summary>
    public TerrainType[] walkablerRegions;

    /// <summary>
    /// 
    /// </summary>
    public int obstaclePenalty = 10;

    /// <summary>
    /// 
    /// </summary>
    LayerMask walkableRegionMask;

    /// <summary>
    /// 
    /// </summary>
    Dictionary<int, int> walkableRegionDictionary = new Dictionary<int, int>();

    /// <summary>
    /// Grid node holds data of a current grid
    /// </summary>
    Node[,] newGrid;

    /// <summary>
    /// Diameter of the node
    /// </summary>
    float nodeLength;

    /// <summary>
    /// grid of the plane x axis
    /// </summary>
    int gridSizeCoordinateX;

    /// <summary>
    /// grid of the plane y axis
    /// </summary>
    int  gridSizeCoordinateY;

    /// <summary>
    /// 
    /// </summary>
    int penaltyMin = int.MaxValue;

    /// <summary>
    /// 
    /// </summary>
    int penaltyMax = int.MinValue;

    #region Unity Methods

    void Awake()
    {
        nodeLength = radiusOfNode * 2;
        gridSizeCoordinateX = Mathf.RoundToInt(gridWorldCoordinateSize.x / nodeLength);
        gridSizeCoordinateY = Mathf.RoundToInt(gridWorldCoordinateSize.y / nodeLength);

        foreach (TerrainType region in walkablerRegions)
        {
            walkableRegionMask.value |=  region.terrainMask.value;
            walkableRegionDictionary.Add((int)Mathf.Log(region.terrainMask.value,2),region.terrainPenalty);
        }

        GenerateGrid();
    }

    #endregion Unity Methods

    /// <summary>
    /// property returuns size of the maximum size of grid 
    /// </summary>
    public int MaxSize
    {
        get
        {
            return gridSizeCoordinateX * gridSizeCoordinateY;
        }
    }

    /// <summary>
    /// Creates the grid of world point co ordinates
    /// </summary>
    void GenerateGrid()
    {
        newGrid = new Node[gridSizeCoordinateX, gridSizeCoordinateY];

        Vector3 worldCoordinatesBottomLeft = (-Vector3.right + transform.position)* gridWorldCoordinateSize.x / 2 - Vector3.forward * gridWorldCoordinateSize.y / 2;

        for (int x = 0; x < gridSizeCoordinateX; x++)
        {
            for (int y = 0; y < gridSizeCoordinateY; y++)
            {
                Vector3 worldPointCoordinates = worldCoordinatesBottomLeft + Vector3.right * (x * nodeLength + radiusOfNode) + Vector3.forward * (y *nodeLength+ radiusOfNode);
                bool canWalkable = !(Physics.CheckSphere(worldPointCoordinates, radiusOfNode,unwalkableMask));

                int movementTerrainPenalty = 0;

                //raycast

                Ray rayPoint = new Ray(Vector3.up * 50+worldPointCoordinates ,  Vector3.down);
                RaycastHit raycastHit;

                float maxDistance = 100;
                if (Physics.Raycast(rayPoint,out raycastHit, maxDistance, walkableRegionMask))
                {
                    walkableRegionDictionary.TryGetValue(raycastHit.collider.gameObject.layer, out movementTerrainPenalty);
                }

                if (!canWalkable)
                {
                    movementTerrainPenalty += obstaclePenalty;
                }

                newGrid[x, y] = new Node(canWalkable, worldPointCoordinates,x,y,movementTerrainPenalty);
            }
        }

        BlurMapPenalty(4);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blurSize"></param>
    void BlurMapPenalty(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize - 1) / 2;

        int[,] penaltiesHorizontalPass = new int[gridSizeCoordinateX, gridSizeCoordinateY];
        int[,] penaltiesVerticalPass = new int[gridSizeCoordinateX, gridSizeCoordinateY];

        for (int y = 0; y < gridSizeCoordinateY; y++)
        {
            for (int x = -kernelExtents; x <= kernelExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesHorizontalPass[0, y] += newGrid[sampleX, y].movementPenalty;
            }

            for (int x = 1; x < gridSizeCoordinateX; x++)
            {
                int removeIndex = Mathf.Clamp( x - kernelExtents - 1,0,gridSizeCoordinateX);
                int addIndex = Mathf.Clamp(x + kernelExtents,0,gridSizeCoordinateX-1);

                penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - newGrid[removeIndex, y].movementPenalty + newGrid[addIndex, y].movementPenalty;

            }
        }

        for (int x = 0; x < gridSizeCoordinateX; x++)
        {
            for (int y = -kernelExtents; y <= kernelExtents; y++)
            {
                int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
            }

            int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
            newGrid[x, 0].movementPenalty = blurredPenalty;

            for (int y = 1; y < gridSizeCoordinateY; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeCoordinateY);
                int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeCoordinateY - 1);

                penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y-1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];

                blurredPenalty =Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                newGrid[x, y].movementPenalty = blurredPenalty;

                if (blurredPenalty>penaltyMax)
                {
                    penaltyMax = blurredPenalty;
                }
                if (blurredPenalty<penaltyMin)
                {
                    penaltyMin = blurredPenalty;
                }
            }
        }
    }

    /// <summary>
    /// Neighbours data
    /// </summary>
    /// <param name="node">current node </param>
    /// <returns>check and add to neighbour node</returns>
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
                int checkX = node.gridofX + x;
                int checkY = node.gridofY + y;
                if (checkX>=0 && checkX<gridSizeCoordinateX && checkY>=0 && checkY<gridSizeCoordinateY)
                {
                    neighbours.Add(newGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Get the currrent node coordinates of the target object
    /// </summary>
    /// <param name="worldPosition">target position</param>
    /// <returns>grid node of x and y</returns>
    public Node NodeFromWorldLocation(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldCoordinateSize.x / 2) / gridWorldCoordinateSize.x;
        float percentY = (worldPosition.z + gridWorldCoordinateSize.y / 2) / gridWorldCoordinateSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeCoordinateX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeCoordinateY - 1) * percentY);

        return newGrid[x, y];
    }

    /// <summary>
    /// Draws Runtime nodes of the grid
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldCoordinateSize.x, 1, gridWorldCoordinateSize.y));

        if (newGrid!=null && displayGridGizmos)
            {
                foreach (Node n in newGrid)
                {
                    Gizmos.color = Color.Lerp(Color.white, Color.black,Mathf.InverseLerp(penaltyMin,penaltyMax,n.movementPenalty));
                    Gizmos.color = (n.walkableRegion) ? Gizmos.color: Color.red;
                    Gizmos.DrawCube(n.worldLocation, Vector3.one * (nodeLength));
                }
			}
    }
}

/// <summary>
///  
/// </summary>
[System.Serializable]
public class TerrainType
{
    public LayerMask terrainMask;
    public int terrainPenalty;
}

