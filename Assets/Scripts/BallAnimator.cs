using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _ballAnim;

    private void OnCollisionEnter(Collision collision)
    {
        _ballAnim.Play("Bouncing", -1, 0f);
    }
}
