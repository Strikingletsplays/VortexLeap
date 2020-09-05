using TMPro;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private CameraController _camera;
    private HelixController _helix;
    private GameObject _addScore;
    private Animator _addScoreAnim;
    private TextMeshProUGUI _addScoreText;

    private void Awake()
    {
        _camera = FindObjectOfType<CameraController>();
        _helix = FindObjectOfType<HelixController>();
        _addScore = FindObjectOfType<Animator>().gameObject;
        _addScoreAnim = _addScore.GetComponent<Animator>();
        _addScoreText = _addScore.GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Increment camera target platform
        _camera.platformCounter++;
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
           
        //Increse Perfect Pass value by 1
        FindObjectOfType<BallController>().perfectPass++;
        //Move camera if there is a next platform
        if (_camera.platformCounter < _helix.spawnedPlatforms.Count)
        {
            _camera.RepositionCamera();
        }
        //Destroy platform
        Destroy();
    }
    void Destroy()
    {
        //Destroy platform 
        transform.GetComponentInChildren<Collider>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            //Adding Random Force
            transform.GetChild(i).GetComponentInChildren<MeshCollider>().enabled = false;
            transform.GetChild(i).GetComponentInChildren<Rigidbody>().isKinematic = false;
            transform.GetChild(i).GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-5, 5), 0, Random.Range(0, 3)), ForceMode.VelocityChange);
        }
        Destroy(GetComponentInChildren<Transform>().gameObject, 1f);
    }
}
