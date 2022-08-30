using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : BaseState
{
    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Waiting State";
    }

    public override void UpdateState(CommandRequestManager manager)
    {
  
    }
}
