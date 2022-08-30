using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITraining : MonoBehaviour
{
    public GameObject[] seekers;

    public AIBaseState aiCurrentState;

    public AITargetState aITargetState = new AITargetState();

    public AIReGroupState aIReGroupState = new AIReGroupState();

    public AIWaitingState aIWaitingState = new AIWaitingState();

    public float reGroupTime;

    void Start()
    {
        aiCurrentState = aIWaitingState;
        aiCurrentState.AIEnterState(this);
    }

    void Update()
    {
        aiCurrentState.AIUpdateState(this);

        reGroupTime += 0.2f * Time.deltaTime;
    }


   public  void AISwitchState(AIBaseState aIBaseState)
    {
        aiCurrentState = aIBaseState;
        aiCurrentState.AIEnterState(this);
    }
}
