using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public Transform target;

    /// <summary>
    /// 
    /// </summary>
    public float speed = 20;

    public float turnDst = 5;

    Path path;

    #region Unity Methods

    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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
            path = new Path(waypoints,transform.position,turnDst);
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator FollowPath()
    {
        while (true)
        {
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
