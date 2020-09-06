using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        CameraController.singleton.platformCounter = 0;
        Gamemanager.singleton.NextLevel();
    }
}
