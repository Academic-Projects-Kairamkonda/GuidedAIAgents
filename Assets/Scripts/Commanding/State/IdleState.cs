using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class IdleState : BaseState
{
    private const int idleStateTime=2;

    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Idle State";

        manager.timeIncreaseSpeed = 1f;
    }

    public override void UpdateState(CommandRequestManager manager)
    {
        if (manager._unitLifeTime>idleStateTime)
        {
            manager.SwitchState(manager._skillState);
        }

    }
}
