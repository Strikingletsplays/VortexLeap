using UnityEngine;

public class CameraController : MonoBehaviour
{
    //For moving the camera
    public int platformCounter = 0;
    private Vector3 targetPosition;

    //offset
    public Vector3 offset = new Vector3 (0,3,-7);

    //Creating a Singleton
    public static CameraController singleton;

    private void Awake()
    {
        //Set singleton
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        //move to new platform
        transform.position = Vector3.Lerp(transform.position, targetPosition, 5f * Time.fixedDeltaTime);
    }
    public void RepositionCamera()
    {
        if (HelixController.singleton.spawnedPlatforms[platformCounter] != null)   //set cameras position to platform
        {
            targetPosition = HelixController.singleton.spawnedPlatforms[platformCounter].transform.position + offset;
        }
    }
}
