using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiedCanvasScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _percentage;

    [SerializeField]
    private HelixController _helixController;

    [SerializeField]
    private CameraController _cameraController;

    private void Update()
    {
        _percentage.text = Mathf.Round((((float)_cameraController.platformCounter / (float)_helixController.allStages[Gamemanager.singleton.currentStage].Platforms.Count)* 100)).ToString()+ "%";
        _helixController.ResetLTP();
    }
}
