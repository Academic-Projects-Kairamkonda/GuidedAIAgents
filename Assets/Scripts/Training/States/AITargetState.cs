using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetState : AIBaseState
{
    public override void AIEnterState(AITraining aITraining)
    {
        unitState = "Target State";

        for (int i = 0; i < aITraining.seekers.Length; i++)
        {
            aITraining.seekers[i].GetComponent<CommandRequestManager>().SwitchState(
                aITraining.seekers[i].GetComponent<CommandRequestManager>()._idleState);
        }
    }

    public override void AIUpdateState(AITraining aITraining)
    {
       
    }
}
