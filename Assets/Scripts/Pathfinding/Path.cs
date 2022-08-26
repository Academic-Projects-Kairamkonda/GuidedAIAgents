using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePath
{
    /// <summary>
    /// 
    /// </summary>
    public readonly Vector3[] lookAtpoints;

    /// <summary>
    /// 
    /// </summary>
    public readonly Line[] turnedInBoundaries;

    /// <summary>
    /// 
    /// </summary>
    public readonly int LineFinishedIndex;

    /// <summary>
    /// 
    /// </summary>
    public readonly int slowedDownCurrentIndex;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wayCoordinatepoints"></param>
    /// <param name="intialPosition"></param>
    /// <param name="turningDistance"></param>
    /// <param name="stopDistance"></param>
    public GeneratePath(Vector3[] wayCoordinatepoints, Vector3 intialPosition, float turningDistance, float stopDistance )
    {
        lookAtpoints = wayCoordinatepoints;
        turnedInBoundaries = new Line[lookAtpoints.Length];
        LineFinishedIndex = turnedInBoundaries.Length - 1;

        Vector2 lastPositionPoint = V3ToV2(intialPosition);

        for (int i = 0; i < lookAtpoints.Length; i++)
        {
            Vector2 currentCoordinatePoints = V3ToV2(lookAtpoints[i]);
            Vector2 directionToPresentPoints = (currentCoordinatePoints - lastPositionPoint).normalized;
            Vector2 turnedBoundariesPoints = (i==LineFinishedIndex)?currentCoordinatePoints:currentCoordinatePoints - directionToPresentPoints*turningDistance;
            turnedInBoundaries[i] = new Line(turnedBoundariesPoints, lastPositionPoint-directionToPresentPoints*turningDistance);
            lastPositionPoint= turnedBoundariesPoints;
        }

        float distanceFromLastPoint = 0;

        for (int i = lookAtpoints.Length-1; i >0; i--)
        {
            distanceFromLastPoint += Vector3.Distance(lookAtpoints[i], lookAtpoints[i - 1]);
            if (distanceFromLastPoint > stopDistance)
            {
                slowedDownCurrentIndex = i;
                break;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="v3"></param>
    /// <returns></returns>
    Vector2 V3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }


    /// <summary>
    /// 
    /// </summary>
    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;

        foreach (Vector3 p in lookAtpoints)
        {
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
        }

        Gizmos.color = Color.white;

        foreach (Line l in turnedInBoundaries)
        {
            l.DrawWithGizmos(10);
        }
    }
}