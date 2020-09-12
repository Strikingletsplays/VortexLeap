using UnityEngine;
using UnityEngine.Advertisements;

public class Gamemanager : MonoBehaviour
{
    //Controllers
    [SerializeField]
    private BallController _ballController;

    //For UI (progress bar)
    [SerializeField]
    private UIManager _uIManager;
    [SerializeField]
    private GameObject _diedCanvas;

    //For setting colors
    [SerializeField]
    private TrailRenderer _ballTrailRenderer;
    [SerializeField]
    private Renderer _ballTrail;
    [SerializeField]
    private Renderer _helixRenderer;
    [SerializeField]
    private ParticleSystem _ballSplash;
    [SerializeField]
    private Renderer _helixGoal;

    //Score & level
    public int bestScore;
    public int score;
    public int currentStage = 0;

    //Creating a Singleton
    public static Gamemanager singleton;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Awake()
    {
        //Initialize adds
        //Advertisement.Initialize("ID");
        //Check singleton
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        //Get highscore from playerprefs
        bestScore = PlayerPrefs.GetInt("Highscore");
    }
    public void SetColors()
    {
        //SETTING COLORS FROM STAGE FILE
        //Setting the colorTrail color
        _ballTrailRenderer.startColor = HelixController.singleton.allStages[currentStage].stageBallColor;
        _ballTrailRenderer.endColor = HelixController.singleton.allStages[currentStage].stageBallColor;
        //Change color of the ball in stage
        _ballTrail.material.color = HelixController.singleton.allStages[currentStage].stageBallColor;
        //Seting the ball splash color
        _ballSplash.startColor = HelixController.singleton.allStages[currentStage].stageBallColor;
        //Seting the Helix Cylinder color
        _helixRenderer.material.SetColor("_BaseColor", HelixController.singleton.allStages[currentStage].helixCylinderColor);
        //CHange color of background of the stage
        Camera.main.backgroundColor = HelixController.singleton.allStages[currentStage].stageBackgroundColor;
        //Set Goal Color
        //_helixGoal.material.SetColor("_BaseColor",HelixController.singleton.allStages[currentStage].stageBallColor);
    }
    private void FixedUpdate()
    {
        _helixGoal.material.SetColor("_BaseColor", _ballController.GetComponent<Renderer>().material.color);
    }
    public void NextLevel()
    {
        if ((currentStage + 1) < HelixController.singleton.allStages.Count)
        {
            //incece Stage counter
            currentStage++;
            HelixController.singleton.LoadStage(currentStage);
            //Reset minY camera
            StartCoroutine(CameraController.singleton.ResetCamera());
            //For UI (progress bar)
            _uIManager.setNumPlatforms();
            SetColors();
            singleton.score = 0;
            _ballController.ResetBall();
        }
    }
    public void RestartLevel()
    {
        //Re-load the stage
        HelixController.singleton.LoadStage(currentStage);
        //Reset minY camera
        StartCoroutine(CameraController.singleton.ResetCamera());

        //Show add
        //Advertisement.Show();

        //Disable death canvas
        _diedCanvas.SetActive(false);
        Time.timeScale = 1;
        //restart scene
        singleton.score = 0;
        _ballController.ResetBall();
        
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        //if highscore
        if(score > bestScore)
        {
            bestScore = score;
            //Store highscore in Playerprefs!
            PlayerPrefs.SetInt("Highscore", score);
        }
        
    }
}
