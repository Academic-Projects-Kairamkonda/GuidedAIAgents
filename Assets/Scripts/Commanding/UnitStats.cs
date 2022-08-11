using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public Transform targetTransform;

    private float lifeTime;
    private int killedTarget;

    void OnEnable()
    {
        targetTransform = GameObject.Find("Target").transform;
        this.GetComponent<Unit>().target = targetTransform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
