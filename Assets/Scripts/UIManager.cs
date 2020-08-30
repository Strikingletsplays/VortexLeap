using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textBest;
    [SerializeField]
    private Slider levelProgression;

    private int numberOfPlatforms;
    private void Start()
    {
        numberOfPlatforms = FindObjectsOfType<PassCheck>().Length;
    }
    // Update is called once per frame
    void Update()
    {
        textBest.text = "Best : " + Gamemanager.singleton.bestScore;
        textScore.text = Gamemanager.singleton.score.ToString();
        levelProgression.value = (float) (Gamemanager.singleton.score / (Gamemanager.singleton.currentLavel + 1)) / numberOfPlatforms;
        Debug.Log((float)(Gamemanager.singleton.score / (Gamemanager.singleton.currentLavel + 1)) / numberOfPlatforms);
    }
}
