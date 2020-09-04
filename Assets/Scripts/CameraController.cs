using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private HelixController _helix;
    public int platformCounter = 0;
    private Vector3 targetPosition;

    //offset
    private Vector3 offset = new Vector3 (0,3,-7);

    // Update is called once per frame
    void Update()
    {
        //(problem at last passCheck ++platformcounter)
        if(platformCounter < _helix.spawnedPlatforms.Count)
            if (_helix.spawnedPlatforms[platformCounter])   //set cameras position to platform
            {
                targetPosition = _helix.spawnedPlatforms[platformCounter].transform.position + offset;
            }
        //move to new platform
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
}
