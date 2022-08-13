using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public Transform targetTransform;
    private Solider GetSolider;

    private float lifeTime=0;
    private int killedTarget;

    void OnEnable()
    {
        GetSolider = this.GetComponentInParent<Solider>();
        targetTransform = GameObject.Find("Target").transform;
        this.GetComponent<Unit>().target = targetTransform;
        //this.GetComponent<Unit>().target = GetSolider.targetLocationOnRoad[Random.Range(0,3)];
    }

    public void GetUnitLifeTime()
    {
        lifeTime += 1 * Time.deltaTime;
    }


}


