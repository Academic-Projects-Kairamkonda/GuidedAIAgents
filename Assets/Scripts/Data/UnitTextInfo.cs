using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitTextInfo : MonoBehaviour
{
    private TextMeshPro unitText;

    public string unitState = " I am looking";

    void Awake()
    {
        unitText = this.GetComponentInChildren<TextMeshPro>();
    }
    void Start()
    {
        unitText.text =unitState.ToString();
    }

    public void SetTextInfo(string name)
    {
        unitText.text = name.ToString();
    }
 
}
