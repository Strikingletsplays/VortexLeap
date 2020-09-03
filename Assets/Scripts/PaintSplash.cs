using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplash : MonoBehaviour
{    
    void Awake()
    {
        //Change color of the paint splash
        GetComponent<Renderer>().material.color = FindObjectOfType<HelixController>().allStages[FindObjectOfType<Gamemanager>().currentLavel].stageBallColor;
    }
}
