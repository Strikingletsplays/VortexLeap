using UnityEngine;

public class PaintSplash : MonoBehaviour
{
    void Awake()
    {
        //Change color of the paint splash
        GetComponent<Renderer>().material.color = FindObjectOfType<HelixController>().allStages[FindObjectOfType<Gamemanager>().currentStage].stageBallColor;
        Destroy(this.gameObject, 10);
    }

}
