using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking: MonoBehaviour
{
    public string agentName;
    public int skillLevel;
    public string SkillState;

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
    public int _rank;

    void Awake()
    {
        _agentLevel = this.transform.Find("Agent Stats/Agent Level").GetComponent<TextMesh>();
        _agentState = this.transform.Find("Agent Stats/State").GetComponent<TextMesh>();
        _agentLifeTime = this.transform.Find("Agent Stats/Lifetime").GetComponent<TextMesh>();
    }

  
}
