using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class BaseState 
{
    /// <summary>
    /// 
    /// </summary>
    public string unitState;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    public abstract void EnterState(CommandRequestManager manager);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    public abstract void UpdateState(CommandRequestManager manager);

}
