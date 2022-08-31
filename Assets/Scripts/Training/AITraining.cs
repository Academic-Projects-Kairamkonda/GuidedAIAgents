using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITraining : MonoBehaviour
{
    public GameObject[] seekers;

    public AIBaseState aiCurrentState;

    public AIWaitingState aIWaitingState = new AIWaitingState();

    public AITargetState aITargetState = new AITargetState();

    public AIReGroupState aIReGroupState = new AIReGroupState();

    public float timeIncrementSpeed = 1;


    public float reGroupTime;

    void Start()
    {
        aiCurrentState = aIWaitingState;
        aiCurrentState.AIEnterState(this);
    }

    void Update()
    {
        aiCurrentState.AIUpdateState(this);

        reGroupTime += timeIncrementSpeed * Time.deltaTime;
    }


   public  void AISwitchState(AIBaseState aIBaseState)
    {
        aiCurrentState = aIBaseState;
        aiCurrentState.AIEnterState(this);
    }

    public void SwitchWaitingState()
    {
        AISwitchState(aIWaitingState);
    }
}
