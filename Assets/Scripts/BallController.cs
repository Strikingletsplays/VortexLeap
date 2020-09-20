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

    //For restarting to startPosition
    private Vector3 _startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive = false;

    //Animator
    [SerializeField]
    private Animator _ballAnimator;

    //For paint splash
    private bool showPaintSplash = true;
    [SerializeField]
    public GameObject splashPaint;

    //BallSplash Particle System
    [SerializeField]
    private ParticleSystem _ballSplash;

    //For Death Canvas
    [SerializeField]
    private GameObject _DiedCanvas;

    //PowerUps
    public bool PowerupSuperSpeed = false;

    public void SpawnBall()
    {
        _startPos = HelixController.singleton.spawnedPlatforms[0].transform.position + new Vector3(0, 2, -1.6f);
        transform.position = _startPos;
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
            //Reset color of ball after collition
            GetComponent<Renderer>().material.color = HelixController.singleton.allStages[Gamemanager.singleton.currentStage].stageBallColor;
            //Destroy Platform
            if (!collision.transform.GetComponent<Goal>())
            {
                Transform parent = collision.transform.parent;

                //Dissable the trigger collider
                parent.GetComponent<Collider>().enabled = false;

                //change platform collor to Ball color and add torque and force!
                for (int i=0; i < parent.childCount; i++)
                {
                    Transform part = parent.GetChild(i);
                    Rigidbody _rb = part.GetComponent<Rigidbody>();
                    part.GetComponent<Renderer>().material.color = HelixController.singleton.allStages[Gamemanager.singleton.currentStage].stageBallColor;
                    part.GetChild(1).GetComponent<MeshCollider>().enabled = false;
                    _rb.isKinematic = false;
                    _rb.AddTorque(new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.VelocityChange);
                    _rb.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.VelocityChange);
                }
                Destroy(parent.gameObject, 2);
                //Add SCORE!!           (Later make Extra score due to superspeed!!)
                Gamemanager.singleton.AddScore(Gamemanager.singleton.currentStage + 1);
                //Increment camera target platform
                CameraController.singleton.platformCounter++;
            }
        }
        else
        {
            //Enable splash Paint
            showPaintSplash = true;

            //check if ball hit a red area and restart lvl
            if (collision.transform.GetComponent<DeathPart>())
            {
                Time.timeScale = 0;
                _DiedCanvas.SetActive(true);
            }
        }

        if (showPaintSplash && !collision.transform.GetComponent<Goal>())
        {
            GameObject splash  = Instantiate(splashPaint, new Vector3(transform.position.x, collision.transform.position.y + 0.38f, transform.position.z), Quaternion.Euler(new Vector3 (90, Random.Range(0,360) ,0)));
            splash.transform.parent = collision.transform;
        }

        //play Ball Splash Particle System
        _ballSplash.GetComponent<SplashController>().MoveToPlatform(collision.transform);
        _ballSplash.Play();
        //if you dont have SSPower-up, bounce the ball
        if (!PowerupSuperSpeed)
        {
            //adding force to ball UP (bounce up)
            _ballRb.velocity = Vector3.zero;
            _ballRb.AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);
        }

        _ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        //Reset PP & SS
        perfectPass = 0;
        isSuperSpeedActive = false;
        //Set animator bool value to false
        _ballAnimator.SetBool("SuperSpeed", false);
    }

    private void FixedUpdate()
    {
        //check for superspeed!
        if(perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            _ballRb.AddForce(Vector3.down * 0.5f, ForceMode.Impulse);
            SSColorchangeBall();
            //make particles part! (todo)
        }
        //Powerup SuperSpeed
        if (PowerupSuperSpeed)
        {
            isSuperSpeedActive = true;
            //change color to red
            SSColorchangeBall();
            //adding force to ball Down
            _ballRb.AddForce(Vector3.down, ForceMode.Acceleration);
            GetComponent<SphereCollider>().isTrigger = true;
        }
        else
        {
            //Enable collider
            GetComponent<SphereCollider>().isTrigger = false;
        }

    }
    private void SSColorchangeBall()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(HelixController.singleton.allStages[Gamemanager.singleton.currentStage].stageBallColor, Color.red, 0.5f);
        //Set animator SuperSpeed value to true
        _ballAnimator.SetBool("SuperSpeed", true);
    }
    private void AllowCollision()
    {
        _ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        //Reset camera possition to start possition
        transform.position = _startPos;
        //Reset platform counter (for %) & Camera to starting position
        CameraController.singleton.platformCounter = 0;
        CameraController.singleton.gameObject.transform.position = new Vector3 (0, 7f, 0);
    }
}
