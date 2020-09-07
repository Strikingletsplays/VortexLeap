using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Gamemanager.singleton.NextLevel();
    }
}
