using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReGroupingState : BaseState
{
    private float currrentlifeTime;

    private const float speedIncrement = 0.5f;

    public override void EnterState(CommandRequestManager manager)
    {
        manager.GetUnit.target = manager.parentTransform;
        manager.GetUnit.StartPath();

        unitState = "Re Grouping State";
        manager.timeIncreaseSpeed = speedIncrement;
    }

    public override void UpdateState(CommandRequestManager manager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(manager.transform.position, 3f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.GetComponent<AITraining>())
            {
                manager.GetUnit.StopPath();
                manager.SwitchState(manager._waitingState);
            }
        }
    }

}
