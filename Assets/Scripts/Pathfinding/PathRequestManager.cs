using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    Queue<PathResult> results = new Queue<PathResult>();

    /// <summary>
    /// 
    /// </summary>
    PathFinding pathFinding;

    /// <summary>
    /// 
    /// </summary>
    static PathRequestManager instance;

    #region Unity Methods

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }


    void Update()
    {
        if (results.Count>0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }
    
    #endregion Unity Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathStart"></param>
    /// <param name="pathEnd"></param>
    /// <param name="callback"></param>
    public static void EntityRequest(EntityRequestPath request)
    {
        ThreadStart newThreadStart= delegate
        {
            instance.pathFinding.GenerateFindPath(request, instance.FinishedProcessingPath);
	    };

        newThreadStart.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="sucess"></param>
    public void FinishedProcessingPath(PathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);

        }
    }


}                       

/// <summary>
/// 
/// </summary>
public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] _path,bool _success, Action<Vector3[], bool> _callback)
    {
        path = _path;
        success = _success;
        callback = _callback;
    }
}
/// <summary>
/// 
/// </summary>
public struct EntityRequestPath
{
    public Vector3 initialPath;
    public Vector3 finalPath;
    public Action<Vector3[], bool> callback;

    public EntityRequestPath( Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        initialPath = _start;
        finalPath = _end;
        callback = _callback;
    }
}