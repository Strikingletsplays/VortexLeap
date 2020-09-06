using TMPro;
using UnityEngine;

public class DiedCanvasScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _percentage;

    private void Update()
    {
        _percentage.text = Mathf.Round((((float)CameraController.singleton.platformCounter / (float)HelixController.singleton.allStages[Gamemanager.singleton.currentStage].Platforms.Count)* 100)).ToString()+ "%";
        HelixController.singleton.ResetLTP();
    }
}
