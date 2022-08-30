using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRequestManager : MonoBehaviour
{
    public IdleState _idleState = new IdleState();
    public SkillState _skillState = new SkillState();
    public RerouteState _rerouteState = new RerouteState();
    public ReGroupingState _reGroupingState = new ReGroupingState();

    /// <summary>
    /// 
    /// </summary>
    public BaseState currentState;

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

    public Unit GetUnit;

    public Transform parentTransform;


    void Awake()
    {
        GetUnit = this.GetComponent<Unit>();
        parentTransform = this.transform.parent.GetComponent<TargetRequestManager>()._checkPoints[0];
    }

    void Start()
    {
        currentState = _idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);

        _unitLifeTime += timeIncreaseSpeed * Time.deltaTime;
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public bool TimerLogic(float maxTime, float currentTime)
    {
        if(maxTime>currentTime)
        {
            return true;
        }

        return false;
    }

}
