using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = HelixController.singleton.allStages[Gamemanager.singleton.currentStage].deathPartColor;
    }
}
