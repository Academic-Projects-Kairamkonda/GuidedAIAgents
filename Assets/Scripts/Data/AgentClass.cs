using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentClass : MonoBehaviour
{
    public AgentType agentType;

    public Transform target;


    /*
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.transform.GetComponent<Unit>())
        {
            Debug.Log("Entered into collision state");

            other.transform.GetComponent<Unit>().target = this.target;
            other.transform.GetComponent<Unit>().IntitatePath(target);
        }
    }
    */
}

[System.Serializable]
public enum AgentType
{
    solider,
    officer
}
