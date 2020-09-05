using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    [SerializeField]
    private BallController _ballController;
    private Vector3 offset = new Vector3(0, 0.5f, -1.4f);

    public void MoveToPlatform(Transform Platform)
    {
        transform.position = Platform.position + offset;
    }
}
