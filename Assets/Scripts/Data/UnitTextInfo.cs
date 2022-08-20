using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitTextInfo : MonoBehaviour
{
    public TextMeshPro unitText;

    private string unitState = " Empty";

    public string UnitState
    {
        get
        {
            return unitState;
        }

        set
        {
            unitState = value;
        }
    }

    public static UnitTextInfo instance;

    void Awake()
    {
        instance = this;
        unitText = this.GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        unitText.text =unitState.ToString();
    }
}
