using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private CameraController _camera;
    private HelixController _helix;

    private void Awake()
    {
        _camera = FindObjectOfType<CameraController>();
        _helix = FindObjectOfType<HelixController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Increment camera target platform
        _camera.platformCounter++;
        //Adding the lvl value to score
        Gamemanager.singleton.AddScore(Gamemanager.singleton.currentLevel + 1);
        //Increse Perfect Pass value by 1
        FindObjectOfType<BallController>().perfectPass++;
        //Move camera if there is a next platform
        if (_camera.platformCounter < _helix.spawnedPlatforms.Count)
        {
            _camera.RepositionCamera();
        }
        //Destroy platform
        Destroy();
    }
    void Destroy()
    {
        //Destroy platform
        GetComponentInChildren<Collider>().enabled = false;
        Destroy(GetComponentInChildren<Transform>().gameObject, 0.3f);
        System.GC.Collect();
    }
}
