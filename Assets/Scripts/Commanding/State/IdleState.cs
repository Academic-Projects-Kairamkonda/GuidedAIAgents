using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class IdleState : BaseState
{
    private const int idleStateTime=2;

    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        //Debug.Log("Entered into Idle State");
        unitState = "Idle State";

        commandRequestManager.timeIncreaseSpeed = 0.2f;

        //commandRequestManager._unit.target = commandRequestManager.targets[0];
    }

    public override void OnCollisionEnter(CommandRequestManager predator, Collision collision)
    {
        Debug.Log("Enter into Idle collision State");
    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        if (commandRequestManager._unitLifeTime>idleStateTime)
        {
            commandRequestManager.SwitchState(commandRequestManager._skillState);
        }

    }
}
