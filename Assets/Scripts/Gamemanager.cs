using UnityEngine;
using UnityEngine.Advertisements;

public class Gamemanager : MonoBehaviour
{
    //Controllers
    [SerializeField]
    private HelixController _helixController;
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

    //Score & level
    public int bestScore;
    public int score;
    public int currentStage = 0;

    public static Gamemanager singleton;

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
        _ballTrailRenderer.startColor = _helixController.allStages[currentStage].stageBallColor;
        _ballTrailRenderer.endColor = _helixController.allStages[currentStage].stageBallColor;
        //Change color of the ball in stage
        _ballTrail.material.color = _helixController.allStages[currentStage].stageBallColor;
        //Seting the ball splash color
        _ballSplash.startColor = _helixController.allStages[currentStage].stageBallColor;
        //Seting the Helix Cylinder color
        _helixRenderer.material.SetColor("_BaseColor", _helixController.allStages[currentStage].helixCylinderColor);
        //CHange color of background of the stage
        Camera.main.backgroundColor = _helixController.allStages[currentStage].stageBackgroundColor;
    }
    public void NextLevel()
    {
        if ((currentStage + 1) < _helixController.allStages.Count)
        {
            currentStage++;
            _helixController.LoadStage(currentStage);
            //For UI (progress bar)
            _uIManager.setNumPlatforms();
            SetColors();
            singleton.score = 0;
            _ballController.ResetBall();
        }
    }
    public void RestartLevel()
    {
        //Show add
        //Advertisement.Show();

        //Disable death canvas
        _diedCanvas.SetActive(false);
        Time.timeScale = 1;
        //restart scene
        singleton.score = 0;
        _ballController.ResetBall();
        _helixController.LoadStage(currentStage);
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
