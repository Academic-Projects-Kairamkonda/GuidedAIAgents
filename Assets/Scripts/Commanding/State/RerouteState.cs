using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RerouteState : BaseState
{
    /// <summary>
    /// 
    /// </summary>
    private float holdonTime = 3;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    public override void EnterState(CommandRequestManager manager)
    {
        Debug.Log("Entered into a reoute state");

        manager.GetUnit.StopMovement();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    public override void UpdateState(CommandRequestManager manager)
    {
        
    }
}
