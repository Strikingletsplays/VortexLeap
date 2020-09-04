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

    //Level Progration
    [SerializeField]
    private TextMeshProUGUI _currentLvl;
    [SerializeField]
    private TextMeshProUGUI _nextLvl;
    [SerializeField]
    private Slider levelProgression;

    private int numberOfPlatforms;
    private void Awake()
    {
        numberOfPlatforms = FindObjectsOfType<PassCheck>().Length;
    }
    // Update is called once per frame
    void Update()
    {
        int score = Gamemanager.singleton.score;
        int currentLevel = Gamemanager.singleton.currentLevel;

        _currentLvl.text = (currentLevel + 1).ToString();
        _nextLvl.text = (currentLevel + 2).ToString();

        textBest.text = "Best : " + Gamemanager.singleton.bestScore;
        textScore.text = score.ToString();
        levelProgression.value = (float) (score / (currentLevel + 1)) / numberOfPlatforms;
    }
}
