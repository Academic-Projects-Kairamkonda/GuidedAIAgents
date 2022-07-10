using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    private Vector3 offset;
    public Transform targetPos;
    private Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        offset = targetPos.transform.position - currentPos;

        offset = new Vector3(offset.x, 10, offset.z);

        transform.position = currentPos + offset;
    }
}
