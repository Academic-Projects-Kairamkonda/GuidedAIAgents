using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RerouteState : BaseState
{
    private float holdonTime = 3;

    public override void EnterState(CommandRequestManager commandRequestManager)
    {
        Debug.Log("Entered into a reoute state");

        commandRequestManager._unit.target = commandRequestManager._targetRequestManager._checkPoints[Random.Range(0, 2)];
        commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);

    }

    public override void UpdateState(CommandRequestManager commandRequestManager)
    {

        if (holdonTime < 1 * Time.deltaTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(commandRequestManager.transform.position, 3f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.GetComponent<GuideAnotherTarget>())
                {
                    commandRequestManager._unit.target = hitCollider.transform.GetComponent<GuideAnotherTarget>().checkpoints[0];
                    commandRequestManager._unit.IntitatePath(commandRequestManager._unit.target);
                }
            }
        }
    }
}
