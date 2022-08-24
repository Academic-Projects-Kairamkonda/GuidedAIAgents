using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    public string unitState;
    
    public abstract void EnterState(CommandRequestManager commandRequestManager);

    public abstract void UpdateState(CommandRequestManager commandRequestManager);

}
