using System.Linq;
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
    private CameraController _Camera;
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

    [SerializeField]
    private HelixController _helixController;

    private void Awake()
    {
        setNumPlatforms();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score = Gamemanager.singleton.score;
        currentLevel = Gamemanager.singleton.currentLevel;

        _currentLvl.text = (currentLevel + 1).ToString();
        _nextLvl.text = (currentLevel + 2).ToString();

        textBest.text = "Best : " + Gamemanager.singleton.bestScore;
        textScore.text = score.ToString();
        levelProgression.value = (float) (_Camera.platformCounter) / numberOfPlatforms;
    }
    public void setNumPlatforms()
    {
        numberOfPlatforms = _helixController.spawnedPlatforms.Count;
    }
}
