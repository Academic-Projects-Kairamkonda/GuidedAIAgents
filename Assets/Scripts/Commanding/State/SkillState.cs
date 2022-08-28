using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SkillState : BaseState
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandRequestManager"></param>
    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        unitState = "Skill State";

        commandRequestManager._rank++;
        commandRequestManager.timeIncreaseSpeed = 1f;
        commandRequestManager._unit.target=commandRequestManager._targetRequestManager._checkPoints[0];
       commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandRequestManager"></param>
    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(commandRequestManager.transform.position, 3f);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.transform.GetComponent<GuideAnotherTarget>())
            {
                commandRequestManager._unit.StopPath();
                commandRequestManager.SwitchState(commandRequestManager._rerouteState);
            }
        }
    }
}
