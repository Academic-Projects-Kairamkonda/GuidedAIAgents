using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CommandRequestManager : MonoBehaviour
{
    public BaseState currentState;
    public TextMesh _agentLevel;
    public TextMesh _agentState;
    public TextMesh _agentLifeTime;

    public int _rank=1;

    public IdleState _idleState = new IdleState();
    public SkillState _skillState = new SkillState();

    void Awake()
    {
        _agentLevel = this.transform.Find("Agent Stats/Agent Level").GetComponent<TextMesh>();
        _agentState = this.transform.Find("Agent Stats/State").GetComponent<TextMesh>();
        _agentLifeTime= this.transform.Find("Agent Stats/Lifetime").GetComponent<TextMesh>();
    }

    void Start()
    {
        currentState = _idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        UnitTextUpdate();
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void UnitTextUpdate()
    {
        _agentLevel.text = $"Agent Level {_rank}";
        _agentState.text= currentState.unitState;
        _agentLifeTime.text = Mathf.RoundToInt(_idleState.unitLifeTime).ToString();
    }

}
