using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIReGroupState : AIBaseState
{
    public override void AIEnterState(AITraining aITraining)
    {
        unitState="Re Grouping State";

        for (int i = 0; i < aITraining.seekers.Length; i++)
        {
            aITraining.seekers[i].GetComponent<CommandRequestManager>().SwitchState(
                aITraining.seekers[i].GetComponent<CommandRequestManager>()._reGroupingState);
        }
    }

    public override void AIUpdateState(AITraining aITraining)
    {



    }
}
