using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool _ignoreNextCollision;

    //RigidBody of Ball
    private Rigidbody _ballRb;

    [SerializeField]
    private float _impulseForce = 25f;

    //For restarting to startPosition
    private Vector3 _startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    //Camera counter (platform reset)
    private CameraController camera;

    //For paint splash
    private bool showPaintSplash = true;
    [SerializeField]
    public GameObject splashPaint;


    void Awake()
    {
        _startPos = transform.position;
        _ballRb = GetComponent<Rigidbody>();
        camera = FindObjectOfType<CameraController>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (_ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            //Disable splash Paint
            showPaintSplash = false;
            //Destroy Platform
            if (!collision.transform.GetComponent<Goal>())
            {
                //change platform collor to Ball color Before destroy
                for(int i=0; i < collision.transform.parent.childCount; i++)
                {
                    collision.transform.parent.GetChild(i).GetComponent<Renderer>().material.color = this.gameObject.GetComponent<Renderer>().material.color;
                    collision.transform.parent.GetChild(i).GetComponent<MeshCollider>().enabled = false;
                    collision.transform.parent.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    collision.transform.parent.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.up * 10, ForceMode.VelocityChange);
                }
                Destroy(collision.transform.parent.gameObject, 0.3f);
                //Increse camera platform counter.
                StartCoroutine(moveCameraCounter());
                //Add SCORE!!           (Later make Extra score due to superspeed!!)
                Gamemanager.singleton.AddScore(Gamemanager.singleton.currentLavel + 1);
                //make particles part!
            }
        }
        else
        {
            //Enable splash Paint
            showPaintSplash = true;
            //check if ball hit a red area and restart lvl
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                deathPart.HitDeathPart();
            }
        }

        if (showPaintSplash && !collision.transform.GetComponent<Goal>())
        {
            GameObject splash  = Instantiate(splashPaint, new Vector3 (transform.position.x, transform.position.y -0.17f , transform.position.z), Quaternion.Euler(new Vector3 (90, 0 ,0)));
            splash.transform.parent = collision.transform;
        }

        //adding force to ball UP (bounce up)
        _ballRb.velocity = Vector3.zero;
        _ballRb.AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);

        _ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        //check for superspeed!
        if(perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            _ballRb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }
    private void AllowCollision()
    {
        _ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = _startPos;
        //Reset Camera to starting position
        FindObjectOfType<Camera>().transform.position = new Vector3 (0,8,-7);
        //Reset platform counter
        camera.platformCounter = 0;
    }
    IEnumerator moveCameraCounter()
    {
        yield return new WaitForSeconds(1);
        camera.platformCounter++;
        yield return null;
    }
}
