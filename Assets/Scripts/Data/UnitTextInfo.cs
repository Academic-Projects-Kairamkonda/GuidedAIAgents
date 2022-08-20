using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitTextInfo : MonoBehaviour
{
    public TextMesh unitText;

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
        unitText = this.GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        unitText.text =unitState.ToString();
    }
}
