using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    private Rigidbody _rb;
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = HelixController.singleton.allStages[Gamemanager.singleton.currentStage].deathPartColor;
    }
    private void Update()
    {
        if (!_rb.isKinematic)
        {
            GetComponentInChildren<Collider>().enabled = false;
        }
    }
}
