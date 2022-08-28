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
    /// <param name="commandRequestManager"></param>
    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        Debug.Log("Entered into a reoute state");

        commandRequestManager._unit.target = commandRequestManager._targetRequestManager._checkPoints[1];
        commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandRequestManager"></param>
    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        if (holdonTime < 1 * Time.deltaTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(commandRequestManager.transform.position, 3f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.GetComponent<GuideAnotherTarget>())
                {
                    Debug.Log("Found target");

                    /*
                    commandRequestManager._unit.target = hitCollider.transform.GetComponent<GuideAnotherTarget>().checkpoints[0];
                    commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);
                    */
                }
            }
        }
    }
}
