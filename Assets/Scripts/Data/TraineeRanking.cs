using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraineeRanking : Ranking
{
    /// <summary>
    /// 
    /// </summary>
    private CommandRequestManager commandRequestManager;

    // Start is called before the first frame update
    void Start()
    {
        commandRequestManager = this.GetComponent<CommandRequestManager>();
    }


    // Update is called once per frame
    void Update()
    {
        UnitTextUpdate();
    }

    private void UnitTextUpdate()
    {
        _agentLevel.text = $"Agent Level {commandRequestManager._rank}";
        _agentState.text = commandRequestManager.currentState.unitState;
        _agentLifeTime.text = $"Life time: {Mathf.RoundToInt(commandRequestManager._unitLifeTime).ToString()}";
    }
}
