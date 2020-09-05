using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private CameraController _cameraController;
    private void OnCollisionEnter(Collision collision)
    {
        _cameraController.platformCounter = 0;
        Gamemanager.singleton.NextLevel();
    }
}
