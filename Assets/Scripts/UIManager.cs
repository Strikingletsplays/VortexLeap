using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textBest;

    // Update is called once per frame
    void Update()
    {
        textBest.text = "Best : " + Gamemanager.singleton.bestScore;
        textScore.text = Gamemanager.singleton.score.ToString();
    }
}
