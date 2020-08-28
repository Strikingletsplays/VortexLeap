using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Gamemanager.singleton.AddScore(1);
        FindObjectOfType<BallController>().perfectPass++;
    }
}
