using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class IdleState : BaseState
{
    private const int idleStateTime=2;

    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        unitState = "Idle State";

        commandRequestManager.timeIncreaseSpeed = 1f;
    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        if (commandRequestManager._unitLifeTime>idleStateTime)
        {
            commandRequestManager.SwitchState(commandRequestManager._skillState);
        }

    }
}
