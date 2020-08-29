using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Gamemanager.singleton.AddScore(Gamemanager.singleton.currentLavel);
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
