using UnityEngine;

public class SplashController : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0.5f, -1.6f);

    public void MoveToPlatform(Transform Platform)
    {
        transform.position = Platform.position + offset;
    }
}
