using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private HelixController _helix;
    public int platformCounter = 0;
    private Vector3 targetPosition;

    //offset
    public Vector3 offset = new Vector3 (0,3,-7);

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        //move to new platform
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
    public void RepositionCamera()
    {
        if (_helix.spawnedPlatforms[platformCounter])   //set cameras position to platform
        {
            targetPosition = _helix.spawnedPlatforms[platformCounter].transform.position + offset;
        }
    }
}
