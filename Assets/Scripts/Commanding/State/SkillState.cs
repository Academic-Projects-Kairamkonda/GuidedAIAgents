using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : BaseState
{
    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        unitState = "Skill State";

        commandRequestManager._rank++;
        commandRequestManager.timeIncreaseSpeed = 1f;
        commandRequestManager._unit.target=commandRequestManager._targetRequestManager._checkPoints[Random.Range(0, 2)];
        commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);
    }


    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(commandRequestManager.transform.position, 3f);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.transform.GetComponent<GuideAnotherTarget>())
            {
                /*
                commandRequestManager._unit.target = hitCollider.transform.GetComponent<GuideAnotherTarget>().checkpoints[0];
                commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);
                */
                commandRequestManager._unit.StopPath();
                commandRequestManager.SwitchState(commandRequestManager._rerouteState);
            }
        }
    }
}
