using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Adding the lvl value to score
        Gamemanager.singleton.AddScore(Gamemanager.singleton.currentLavel+1);
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
