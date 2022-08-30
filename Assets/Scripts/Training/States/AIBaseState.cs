using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBaseState 
{
    public string unitState;

    public abstract void AIEnterState(AITraining aITraining);

    public abstract void AIUpdateState(AITraining aITraining);

}
