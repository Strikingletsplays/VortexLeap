using System.Collections;
using TMPro;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private GameObject _addScore;
    private Animator _addScoreAnim;
    private TextMeshProUGUI _addScoreText;

    private void Awake()
    {
        _addScore = GameObject.FindGameObjectWithTag("AddScore");
        _addScoreAnim = _addScore.GetComponent<Animator>();
        _addScoreText = _addScore.GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Increment camera target platform
        CameraController.singleton.platformCounter++;
        //Adding the lvl value to score
        Gamemanager.singleton.AddScore(Gamemanager.singleton.currentStage + 1);
        //Play AnimScore 
        if (_addScoreAnim.GetBool("AddScore"))
        {
            _addScoreText.text = "+" + (int.Parse(_addScoreText.text) + (Gamemanager.singleton.currentStage + 1)).ToString();
            _addScoreAnim.Play("AddScore",-1,0);
        }
        else
        {
            _addScoreText.text = "+" + (Gamemanager.singleton.currentStage + 1).ToString();
            _addScoreAnim.SetBool("AddScore", true);
        }

        BallController ball = FindObjectOfType<BallController>();
        if (!ball.isSuperSpeedActive)
        {
            //Increse Perfect Pass value by 1
            ball.perfectPass++;
        }
        //Destroy platform
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        //disable the colliders
        transform.GetComponentInChildren<Collider>().enabled = false;

        if (!FindObjectOfType<BallController>().PowerupSuperSpeed)
        {
            //Destroy platform 
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform platform = transform.GetChild(i);
                Rigidbody _rb = platform.GetComponent<Rigidbody>();
                //Adding Random Force
                platform.GetChild(1).GetComponent<MeshCollider>().enabled = false;
                _rb.isKinematic = false;
                _rb.AddForce(new Vector3(0, 2, 2), ForceMode.VelocityChange);
                _rb.AddTorque(new Vector3(Random.Range(-2, 2), 0, Random.Range(0, 1)), ForceMode.VelocityChange);
            }
        }
        else
        {
            //Destroy platform 
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform platform = transform.GetChild(i);
                Rigidbody _rb = platform.GetComponent<Rigidbody>();
                //Adding Random Force
                platform.GetChild(1).GetComponent<MeshCollider>().enabled = false;
                _rb.isKinematic = false;
                _rb.AddForce(new Vector3(0, -10, 2), ForceMode.VelocityChange);
                _rb.AddTorque(new Vector3(Random.Range(-2, 2), 0, Random.Range(0, 1)), ForceMode.VelocityChange);
            }
        }
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);


    }
}
