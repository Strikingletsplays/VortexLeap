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

    //For setting colors
    [SerializeField]
    private TrailRenderer _ballTrailRenderer;
    [SerializeField]
    private Renderer _ballTrail;
    [SerializeField]
    private Renderer _helixRenderer;

    //Score & level
    public int bestScore;
    public int score;
    public int currentLevel = 0;

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

        //Set Framerate
        Application.targetFrameRate = 60;
    }
    public void SetColors()
    {
        //SETTING COLORS FROM STAGE FILE
        //Setting the colorTrail color
        _ballTrailRenderer.startColor = _helixController.allStages[currentLevel].stageBallColor;
        _ballTrailRenderer.endColor = _helixController.allStages[currentLevel].stageBallColor;
        //Change color of the ball in stage
        _ballTrail.material.color = _helixController.allStages[currentLevel].stageBallColor;
        //Seting the Helix Cylinder color
        _helixRenderer.material.color = _helixController.allStages[currentLevel].helixCylinderColor;
        //CHange color of background of the stage
        Camera.main.backgroundColor = _helixController.allStages[currentLevel].stageBackgroundColor;
    }
    public void NextLevel()
    {
        if ((currentLevel+1) < _helixController.allStages.Count)
        {
            currentLevel++;
            _helixController.LoadStage(currentLevel);
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
        Advertisement.Show();
        //restart scene
        singleton.score = 0;
        _ballController.ResetBall();
        _helixController.LoadStage(currentLevel);
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
