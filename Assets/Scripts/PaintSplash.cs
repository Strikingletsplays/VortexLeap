using UnityEngine;

public class PaintSplash : MonoBehaviour
{
    void Awake()
    {
        //Change color of the paint splash
        GetComponent<Renderer>().material.color = HelixController.singleton.allStages[Gamemanager.singleton.currentStage].stageBallColor;
        Destroy(this.gameObject, 10);
    }

}