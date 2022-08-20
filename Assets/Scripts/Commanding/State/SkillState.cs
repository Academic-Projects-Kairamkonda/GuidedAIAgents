using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : BaseState
{
    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        Debug.Log($"Entered in {commandRequestManager.currentState}");
        unitState = "Skill State";

        commandRequestManager._rank++;
    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {
        
    }
}
