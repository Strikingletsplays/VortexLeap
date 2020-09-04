using UnityEngine;
using UnityEngine.Advertisements;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    private HelixController _helixController;
    [SerializeField]
    private BallController _ballController;

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
    public void NextLevel()
    {
        if (currentLevel < _helixController.allStages.Count)
        {
            currentLevel++;
            _helixController.LoadStage(currentLevel);
        }
        singleton.score = 0;
        _ballController.ResetBall();
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
