using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //For no accidental double collisions
    private bool _ignoreNextCollision;

    //RigidBody of Ball
    [SerializeField]
    private Rigidbody _ballRb;

    [SerializeField]
    private float _impulseForce;

    [SerializeField]
    private HelixController _helix;

    //For restarting to startPosition
    private Vector3 _startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    //Camera counter (platform reset)
    [SerializeField]
    private CameraController _camera;

    //For paint splash
    private bool showPaintSplash = true;
    [SerializeField]
    public GameObject splashPaint;

    //BallSplash Particle System
    [SerializeField]
    private ParticleSystem _ballSplash;


    void Awake()
    {
        _startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //So it dosent jump to heaven
        if (_ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            //Disable splash Paint
            showPaintSplash = false;
            //Destroy Platform
            if (!collision.transform.GetComponent<Goal>())
            {
                Transform parent = collision.transform.parent;
                //change platform collor to Ball color Before destroy
                for (int i=0; i < collision.transform.parent.childCount; i++)
                {
                    parent.GetChild(i).GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
                    parent.GetChild(i).GetComponent<MeshCollider>().enabled = false;
                    parent.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    parent.GetChild(i).GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.VelocityChange);
                    parent.GetChild(i).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.VelocityChange);
                }
                Destroy(parent.gameObject, 2);
                //Increse camera platform counter.
                StartCoroutine(moveCameraCounter());
                //Add SCORE!!           (Later make Extra score due to superspeed!!)
                Gamemanager.singleton.AddScore(Gamemanager.singleton.currentStage + 1);
                //make particles part! (todo)

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
                Gamemanager.singleton.RestartLevel();
            }
        }

        if (showPaintSplash && !collision.transform.GetComponent<Goal>())
        {
            GameObject splash  = Instantiate(splashPaint, new Vector3 (transform.position.x, transform.position.y -0.15f , transform.position.z), Quaternion.Euler(new Vector3 (90, Random.Range(0,360) ,0)));
            splash.transform.parent = collision.transform;
        }

        //play Ball Splash Particle System
        _ballSplash.GetComponent<SplashController>().MoveToPlatform(collision.transform);
        _ballSplash.Play();
        //adding force to ball UP (bounce up)
        _ballRb.velocity = Vector3.zero;
        _ballRb.AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);

        _ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void FixedUpdate()
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
        //Reset platform counter
        _camera.platformCounter = 0;
        //Reset Camera to starting position
        _camera.gameObject.transform.position = new Vector3 (0,8,-7);
    }
    IEnumerator moveCameraCounter()
    {
        yield return new WaitForSeconds(1);
        if (_camera.platformCounter < _helix.spawnedPlatforms.Count)
            _camera.RepositionCamera();
        yield return null;
    }
}
