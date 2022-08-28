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
    /// <param name="commandRequestManager"></param>
    public abstract void EnterState(CommandRequestManager commandRequestManager);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandRequestManager"></param>
    public abstract void UpdateState(CommandRequestManager commandRequestManager);

}
