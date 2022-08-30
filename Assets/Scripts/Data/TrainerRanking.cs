using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerRanking : Ranking
{
    private AITraining aITraining;

    // Start is called before the first frame update
    void Start()
    {
        aITraining = this.GetComponent<AITraining>();
    }

    // Update is called once per frame
    void Update()
    {
        UnitTextUpdate();
    }

    private void UnitTextUpdate()
    {
        _agentLevel.text = $"Agent Level 10";
        _agentState.text = aITraining.aiCurrentState.unitState;
        _agentLifeTime.text = $"Life time: {Mathf.RoundToInt(aITraining.reGroupTime).ToString()}";
    }
}
