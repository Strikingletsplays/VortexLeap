using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //For moving the camera
    public int platformCounter = 0;

    [SerializeField]
    private float _cameraSpeed;

    private bool _trigger = true;

    //Next possition
    private Vector3 nextPosition;

    //offset
    private float offset = 2.75f;

    [SerializeField]
    private BallController _ballController;

    //minimum y of ball
    public float minY;

    //Creating a Singleton
    public static CameraController singleton;


    private void Awake()
    {
        //Set singleton
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        minY = _ballController.transform.position.y;
        //For camera not to be (z=0)
        nextPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_trigger && minY > _ballController.transform.position.y)
        {
            minY = _ballController.transform.position.y;
        }

        nextPosition.y = minY + offset;
        transform.position = Vector3.Lerp(transform.position, nextPosition, _cameraSpeed * Time.fixedDeltaTime);
    }
    public IEnumerator ResetCamera()
    {
        _trigger = false;
        minY = _ballController.transform.position.y;
        yield return new WaitForSeconds(1f);
        _trigger = true;
        yield return null;
    }
}
