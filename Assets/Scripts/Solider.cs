using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float speed=2f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Vector3 targetPos;

    #region Unity Methods

    void Start()
    {
        targetPos = RandPos();
    }

    void FixedUpdate()
    {
        Movement();
        if (transform.position==targetPos)
        {
            targetPos = RandPos();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            targetPos = RandPos();
        }
    }

    #endregion Unity Methods

    /// <summary>
    /// 
    /// </summary>
    private void Movement()
    {
        //use MoveTowards
        // Move our position a step closer to the target.
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    /// <summary>
    /// 
    /// </summary>
    private void ShortestPath()
    {
        //Move A* alogrithm
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckCollision()
    {
        //Blocked Area
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 RandPos()
    {
        Vector3 pos;

        pos = new Vector3(RandValue(), 0.5f,RandValue());
        return pos;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float RandValue()
    {
        float value;

        value = Random.Range(1, 10);

        if((value % 2) == 0)
        {
            value = -value;
        }
               
        return value;
    }
}
