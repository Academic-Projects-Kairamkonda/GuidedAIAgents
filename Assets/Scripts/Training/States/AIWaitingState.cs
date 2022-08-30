using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaitingState : AIBaseState
{
    public override void AIEnterState(AITraining aITraining)
    {
        unitState = "Waitng State";
    }

    public override void AIUpdateState(AITraining aITraining)
    {
        if (aITraining.reGroupTime > 2)
        {
            aITraining.AISwitchState(aITraining.aITargetState);
        }

    }
}
