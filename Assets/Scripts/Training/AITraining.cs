using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITraining : MonoBehaviour
{
    public GameObject[] seekers;
    public bool startTraining;

    CommandRequestManager manager;

    void Awake()
    {

    }

    void Update()
    {
        
    }

    public void TrainSkillState()
    {
        manager = seekers[0].GetComponent<CommandRequestManager>();
        manager.SwitchState(manager._skillState);
    }


}
