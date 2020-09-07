using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    //For Helix Rotation
    private Vector2 _lastTapPos;
    private Vector3 _startRotation;

    //Helix Platforms
    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helicPlatformPrefab;
    public List<GameObject> spawnedPlatforms = new List<GameObject>();

    //To load next stages
    public List<Stage> allStages = new List<Stage>();

    //Helix Distance
    private float helixDistance;

    //Creating a Singleton
    public static HelixController singleton;

    void Awake()
    {
        //Set singleton
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        _startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - goalTransform.localPosition.y - 0.1f;
        LoadStage(0);
    }
    public void ResetLTP()
    {
        _lastTapPos = Vector2.zero;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //current tap possition
            Vector2 curTapPos = Input.mousePosition;

            if(_lastTapPos == Vector2.zero)
            {
                _lastTapPos = curTapPos;
            }
            //how much you moved your finder tap?
            float delta = _lastTapPos.x - curTapPos.x;
            _lastTapPos = curTapPos;

            //Rotate Helix
            transform.Rotate(Vector3.up * delta * 0.5f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ResetLTP();
        }
    }
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber + " found in allStages List. Are all stages assined in the list?");
            return;
        }
        //SETTING COLORS FROM STAGE FILE
        Gamemanager.singleton.SetColors();

        //Reset helix rotation
        transform.localEulerAngles = _startRotation;

        //destroy old lvl if there exist
        foreach (GameObject go in spawnedPlatforms)
        {
            Destroy(go);
        }
        //Recreate the platform list
        spawnedPlatforms = new List<GameObject>();

        //create new platforms variables
        float LevelDistance = helixDistance / stage.Platforms.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.Platforms.Count; i++)
        {
            //create platforms
            spawnPosY -= LevelDistance;
            GameObject platform = Instantiate(helicPlatformPrefab, transform);
            platform.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedPlatforms.Add(platform);

            //Variables to disable platform parts
            int partsToDisable = 12 - stage.Platforms[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            //Disabling parts in Platforms
            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = platform.transform.GetChild(Random.Range(0, platform.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            //Coloring the left over parts with the stage color.
            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in platform.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }
            //Creating the Death Parts
            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.Platforms[i].deathPartCount && i != 0)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!deathParts.Contains(randomPart))
                {
                    //Adding the Script "DeathPart" to the random part
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }
        }
    }
}
