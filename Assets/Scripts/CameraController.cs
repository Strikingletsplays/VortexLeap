using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private HelixController helix;
    public int platformCounter = 0;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (helix.spawnedPlatforms[platformCounter])
        {
            targetPosition = new Vector3(transform.position.x, helix.spawnedPlatforms[platformCounter].transform.position.y + 3, transform.position.z);
        }
        //move to new platform
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
}
