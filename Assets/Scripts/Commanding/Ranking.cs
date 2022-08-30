using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking: MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    public TextMesh _agentLevel;

    /// <summary>
    /// 
    /// </summary>
    public TextMesh _agentState;

    /// <summary>
    /// 
    /// </summary>
    public TextMesh _agentLifeTime;

    /// <summary>
    /// 
    /// </summary>
    private CommandRequestManager commandRequestManager;


    void Awake()
    {
        _agentLevel = this.transform.Find("Agent Stats/Agent Level").GetComponent<TextMesh>();
        _agentState = this.transform.Find("Agent Stats/State").GetComponent<TextMesh>();
        _agentLifeTime = this.transform.Find("Agent Stats/Lifetime").GetComponent<TextMesh>();

        commandRequestManager = this.GetComponent<CommandRequestManager>();
    }


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
