using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    [SerializeField]
    private CameraController _Camera;

    private void Awake()
    {
        _Camera = FindObjectOfType<CameraController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Increment camera target platform
        _Camera.platformCounter++;
        //Adding the lvl value to score
        Gamemanager.singleton.AddScore(Gamemanager.singleton.currentLevel + 1);
        //Increse Perfect Pass value by 1
        FindObjectOfType<BallController>().perfectPass++;
        //Destroy platform
        Destroy();
    }
    void Destroy()
    {
        //Destroy platform
        GetComponentInChildren<Collider>().enabled = false;
        Destroy(GetComponentInChildren<Transform>().gameObject, 0.3f);
    }
}
