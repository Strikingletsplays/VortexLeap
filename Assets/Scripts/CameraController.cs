using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private BallController target;

    [SerializeField]
    private float cameraSpeed;
    private float offset;

    void Awake()
    {
        offset = transform.position.y - target.transform.position.y ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 curPos = transform.position;
        if(curPos.y - target.transform.position.y > 3)
        {
            //setting new (y) position to target position
            curPos.y = target.transform.position.y + offset;
            //Smouthly Lerp
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * cameraSpeed);
        }
    }
}
