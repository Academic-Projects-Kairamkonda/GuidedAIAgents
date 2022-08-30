using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class IdleState : BaseState
{
    private float currentTime;
    private const float waitTime = 4;
    private const float speedIncrement = 0.5f;

    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Idle State";
        manager.timeIncreaseSpeed = speedIncrement;
        currentTime = manager._unitLifeTime + waitTime;
        manager.GetUnit.target = manager.GetUnit.firstTarget;
    }

    public override void UpdateState(CommandRequestManager manager)
    {
        if (manager._unitLifeTime>currentTime)
            manager.SwitchState(manager._skillState);
    }
}
