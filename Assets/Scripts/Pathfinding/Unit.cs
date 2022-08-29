using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a agent to move on walkable regions
/// </summary>
public class Unit : MonoBehaviour
{
    /// <summary>
    /// minimum path update time 
    /// </summary>
    const float minPathUpdateTime = .2f;

    /// <summary>
    /// move threshold when the path is updated
    /// </summary>
    const float pathUpdateMoveThreshold = .5f;

    /// <summary>
    ///  vector position on the map
    /// </summary>
    public Transform target;

    /// <summary>
    ///  move speed of agent with relative to delta time
    /// </summary>
    public float speed = 20;

    /// <summary>
    /// maximum turning distance
    /// </summary>
    public float turnDst = 5;

    /// <summary>
    /// turnspeed of a agent
    /// </summary>
    public float turnSpeed = 3;

    /// <summary>
    /// Distance between last point and target
    /// </summary>
    public float stoppingDst = 10;

    /// <summary>
    /// it is a reference of a path class
    /// </summary>
   GeneratePath path;

    #region Unity Methods

    void Awake() { }

    void Start()
    {
        /* Commented because I want to trigger a movement at a certain point     
            StartCoroutine(UpdatePath()); */
    }

    #endregion Unity Methods

    /// <summary>
    /// Method to trigger the path and update it
    /// </summary>
    /// <param name="newPath">target location</param>
    /// <param name="pathSuccessful">boolean to check and generate new path</param>
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new GeneratePath(waypoints,transform.position,turnDst,stoppingDst);

            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    /// <summary>
    /// start the agent to move
    /// </summary>
    public void IntitatePath(Transform _target)
    {
        PathRequestManager.EntityRequest(new EntityRequestPath(transform.position, _target.position, OnPathFound));
    }

    /// <summary>
    /// Stops following path
    /// </summary>
    public void StopPath()
    {
        StopCoroutine(FollowPath());

        Debug.Log("Function called");
    }

    /// <summary>
    /// Update the Path when it is followed
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad<.3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        PathRequestManager.EntityRequest(new EntityRequestPath(transform.position, target.position, OnPathFound));

        float sqrMoveThresshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
                 
            if ((target.position-targetPosOld).sqrMagnitude>sqrMoveThresshold)
            {
                PathRequestManager.EntityRequest(new EntityRequestPath(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    /// <summary>
    /// Follow towards target location
    /// </summary>
    /// <returns> null </returns>
    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookAtpoints[0]);

        float speedPercent = 1;
        
        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while(path.turnedInBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex==path.LineFinishedIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                if (pathIndex >= path.slowedDownCurrentIndex && stoppingDst > 0)
                { 
                    speedPercent = Mathf.Clamp01(path.turnedInBoundaries[path.LineFinishedIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent<0.01f)
                    {
                        followingPath = false;
                    }
                }
                Quaternion targetRotation = Quaternion.LookRotation(path.lookAtpoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed* speedPercent, Space.Self);

            }

            yield return null;
        }
    }

    /// <summary>
    /// Draws path with help of unity gizmos
    /// </summary>
    public void OnDrawGizmos()
    {
        if (path!=null)
        {
            path.DrawWithGizmos();
        }
    }
}
