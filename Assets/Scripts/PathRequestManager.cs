using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    Queue<PathRequest> pathRequestsQueue = new Queue<PathRequest>();

    /// <summary>
    /// 
    /// </summary>
    PathRequest currentPathRequest;

    /// <summary>
    /// 
    /// </summary>
    PathFinding pathFinding;

    /// <summary>
    /// 
    /// </summary>
    static PathRequestManager instance;

    /// <summary>
    /// 
    /// </summary>
    bool isProcessingPath;

    #region Unity Methods

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    #endregion Unity Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathStart"></param>
    /// <param name="pathEnd"></param>
    /// <param name="callback"></param>
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action <Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestsQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    /// <summary>
    /// 
    /// </summary>
    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestsQueue.Count>0)
        {
            currentPathRequest = pathRequestsQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="sucess"></param>
    public void FinishedProcessingPath(Vector3[] path, bool sucess)
    {
        currentPathRequest.callback(path, sucess);
        isProcessingPath = false;
        TryProcessNext();
    }

    /// <summary>
    /// 
    /// </summary>
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest( Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}                       
