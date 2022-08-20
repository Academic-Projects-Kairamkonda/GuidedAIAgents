using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        Debug.Log("Entered into Idle State");
    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {

    }
}
