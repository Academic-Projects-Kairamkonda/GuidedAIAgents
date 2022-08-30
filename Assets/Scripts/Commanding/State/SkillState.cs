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
    /// <param name="manager"></param>
    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Skill State";

        manager._rank++;
        manager.timeIncreaseSpeed = 1f;

        manager.GetUnit.StartMovement();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    public override void UpdateState(CommandRequestManager manager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(manager.transform.position, 3f);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.transform.GetComponent<GuideAnotherTarget>())
            {
                manager.SwitchState(manager._rerouteState);
            }
        }
    }
}
