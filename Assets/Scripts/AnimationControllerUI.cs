using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerUI : MonoBehaviour
{
    [SerializeField]
    private Animator addScore;
    public void ResetAnimation()
    {
        addScore.SetBool("AddScore", false);
    }
}
