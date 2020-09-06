using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 

    //Score
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textBest;

    //Level Progration Barr
    [SerializeField]
    private TextMeshProUGUI _currentLvl;
    [SerializeField]
    private TextMeshProUGUI _nextLvl;
    [SerializeField]
    private Slider levelProgression;
    public int numberOfPlatforms;

    //Score & Level
    public int currentLevel;
    public int score;

    private void Awake()
    {
        setNumPlatforms();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score = Gamemanager.singleton.score;
        currentLevel = Gamemanager.singleton.currentStage;

        _currentLvl.text = (currentLevel + 1).ToString();
        _nextLvl.text = (currentLevel + 2).ToString();

        textBest.text = "Best : " + Gamemanager.singleton.bestScore;
        textScore.text = score.ToString();
        levelProgression.value = (float) (CameraController.singleton.platformCounter) / numberOfPlatforms;
    }
    public void setNumPlatforms()
    {
        numberOfPlatforms = HelixController.singleton.spawnedPlatforms.Count;
    }

}
