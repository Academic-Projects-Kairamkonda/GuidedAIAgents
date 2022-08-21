using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : BaseState
{
    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        //Debug.Log($"Entered in {commandRequestManager.currentState}");
        unitState = "Skill State";

        commandRequestManager._rank++;

        commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);
    }

    public override void OnCollisionEnter(CommandRequestManager predator, Collision collision)
    {
        GameObject other = collision.gameObject;

        Debug.Log("Enter in collision state");

        if (other.transform.GetComponentInParent<AgentClass>())
        {
            Debug.Log("Change Position");
        }
    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        
    }
}
