using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerouteState : BaseState
{
    private float currrentlifeTime;

    private const float speedIncrement = 0.5f;

    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Re routing State";
        manager.timeIncreaseSpeed = speedIncrement;
        currrentlifeTime += manager._unitLifeTime;
        currrentlifeTime += 4;
    }

    
    public override void UpdateState(CommandRequestManager manager)
    {
        if (manager._unitLifeTime>currrentlifeTime)
        {
            manager.SwitchState(manager._reGroupingState);
        }
    }
}
