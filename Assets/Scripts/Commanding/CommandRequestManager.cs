using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRequestManager : MonoBehaviour
{
    public IdleState _idleState = new IdleState();
    public SkillState _skillState = new SkillState();

    /// <summary>
    /// 
    /// </summary>
    public BaseState currentState;

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
    public Unit _unit;

    /// <summary>
    /// 
    /// </summary>
    public int _rank=1;

    /// <summary>
    /// 
    /// </summary>
    public float _unitLifeTime;

    /// <summary>
    /// 
    /// </summary>
    public float timeIncreaseSpeed=1;

    void Awake()
    {
        _unit = this.GetComponent<Unit>();

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

        _unitLifeTime += timeIncreaseSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
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
        _agentLifeTime.text = $"Life time: {Mathf.RoundToInt(_unitLifeTime).ToString()}";
    }

}
