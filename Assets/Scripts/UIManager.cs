using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textBest;

    // Start is called before the first frame update
    void Start()
    {
        textBest.text = "Best : " + Gamemanager.singleton.bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = Gamemanager.singleton.score.ToString();
    }
}
