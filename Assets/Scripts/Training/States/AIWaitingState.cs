using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaitingState : AIBaseState
{

    private float waitTime = 3;
    private float currentTime;

    public override void AIEnterState(AITraining aITraining)
    {
        unitState = "Waitng State";
        currentTime = aITraining.reGroupTime + waitTime;
    }

    public override void AIUpdateState(AITraining aITraining)
    {
        if (aITraining.reGroupTime > currentTime)
        {
            aITraining.AISwitchState(aITraining.aITargetState);
        }

    }
}
