using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class IdleState : BaseState
{
    public float unitLifeTime;

    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        Debug.Log("Entered into Idle State");
        unitState = "Idle State";
    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        unitLifeTime += 1 * Time.deltaTime;

        //Debug.Log($"Life time of  a agent {unitLifeTime}");

        if (unitLifeTime>3)
        {
            commandRequestManager.SwitchState(commandRequestManager._skillState);
        }

    }
}
