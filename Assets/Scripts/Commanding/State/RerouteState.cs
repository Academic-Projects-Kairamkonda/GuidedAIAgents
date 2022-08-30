using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerouteState : BaseState
{
    private float currentTime;
    private const float waitTime = 10;
    private const float speedIncrement = 0.5f;

    public override void EnterState(CommandRequestManager manager)
    {
        unitState = "Re routing State";
        manager.timeIncreaseSpeed = speedIncrement;
        currentTime = manager._unitLifeTime+waitTime;
    }

    
    public override void UpdateState(CommandRequestManager manager)
    {
        if (manager._unitLifeTime > currentTime)
        {
            manager.parentTransform.GetComponent<AITraining>().AISwitchState(
                manager.parentTransform.GetComponent<AITraining>().aIReGroupState);
        }
        
    }
}
