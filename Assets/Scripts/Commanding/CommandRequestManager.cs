using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRequestManager : MonoBehaviour
{

    public BaseState currentState;

    private IdleState IdleState = new IdleState();

    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }

}
