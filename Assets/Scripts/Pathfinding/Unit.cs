using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    const float minPathUpdateTime = .2f;

    /// <summary>
    /// 
    /// </summary>
    const float pathUpdateMoveThreshold = .5f;

    /// <summary>
    /// 
    /// </summary>
    public Transform target;

    /// <summary>
    /// 
    /// </summary>
    public float speed = 20;

    /// <summary>
    /// 
    /// </summary>
    public float turnDst = 5;

    /// <summary>
    /// 
    /// </summary>
    public float turnSpeed = 3;

    /// <summary>
    /// 
    /// </summary>
    public float stoppingDst = 10;

    /// <summary>
    /// 
    /// </summary>
    Path path;

    private UnitTextInfo unitTextInfo;

    #region Unity Methods

    void Awake()
    {
        unitTextInfo = this.GetComponent<UnitTextInfo>();
    }

    void Start()
    {
        StartCoroutine(UpdatePath());
        unitTextInfo.SetTextInfo("Looking for \n target");
    }

    #endregion Unity Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newPath"></param>
    /// <param name="pathSuccessful"></param>
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints,transform.position,turnDst,stoppingDst);
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdatePath()
    {
        unitTextInfo.SetTextInfo("Rerouting Path");

        if (Time.timeSinceLevelLoad<.3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThresshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
                 
            if ((target.position-targetPosOld).sqrMagnitude>sqrMoveThresshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookpoints[0]);

        float speedPercent = 1;
        
        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex==path.finishLineIndex)
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
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                { 
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent<0.01f)
                    {
                        followingPath = false;
                    }
                }
                Quaternion targetRotation = Quaternion.LookRotation(path.lookpoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed* speedPercent, Space.Self);

                unitTextInfo.SetTextInfo("Following path");
            }

            yield return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnDrawGizmos()
    {
        if (path!=null)
        {
            path.DrawWithGizmos();
        }
    }
}
