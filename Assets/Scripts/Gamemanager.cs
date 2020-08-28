using UnityEngine;
using UnityEngine.Advertisements;

public class Gamemanager : MonoBehaviour
{

    public int bestScore;
    public int score;

    public int currentLavel = 0;

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
    public void NextLevel()
    {
        currentLavel++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentLavel);
    }
    public void RestartLevel()
    {
        //Show add
        Advertisement.Show();
        //restart scene
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentLavel);
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
