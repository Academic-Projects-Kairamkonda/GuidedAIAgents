using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillState : BaseState
{
    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Skill State";
        manager._rank++;
        manager.timeIncreaseSpeed = 1f;
        manager.GetUnit.StartPath();
    }

    public override void UpdateState(CommandRequestManager manager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(manager.transform.position, 3f);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.transform.GetComponent<GuideAnotherTarget>())
            {
                manager.GetUnit.StopPath();
                manager.SwitchState(manager._rerouteState);
            }
        }
    }
}
