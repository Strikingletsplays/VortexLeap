using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool _ignoreNextCollision;

    //RigidBody of Ball
    private Rigidbody _ballRb;
    [SerializeField]
    private float _impulseForce = 5f;
    private Vector3 _startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;


    void Awake()
    {
        _startPos = transform.position;
        _ballRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (_ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            if (!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent.gameObject, 0.3f);
                //make particles part!
            }
        }
        else
        {
            //check if ball hit a red area and restart lvl
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                deathPart.HitDeathPart();
            }
        }


        //adding force to ball
        _ballRb.velocity = Vector3.zero;
        _ballRb.AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);

        _ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
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
    }
}
