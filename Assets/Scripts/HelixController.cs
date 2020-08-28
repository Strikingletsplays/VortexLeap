using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 _lastTapPos;
    private Vector3 _startRotation;

    //edges of Helix
    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helicPlatformPrefab;

    public List<Stage> allStages = new List<Stage>();

    private float helixDistance;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    void Awake()
    {
        _startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - goalTransform.localPosition.y - 0.1f;
        LoadStage(0);
    }

    // Update is called once per frame
    void Update()
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
            transform.Rotate(Vector3.up * delta);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _lastTapPos = Vector2.zero;
        }
    }
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if(stage == null)
        {
            Debug.LogError("No stage " + stageNumber + " found in allStages List. Are all stages assined in the list?");
            return;
        }
        //CHange color of background of the stage
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        //Change color of the ball in stage
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        //Reset helix rotation
        transform.localEulerAngles = _startRotation;

        //destroy old lvl if there exist
        foreach(GameObject go in spawnedPlatforms)
        {
            Destroy(go);
        }

        //create new platforms
        float LevelDistance = helixDistance / stage.Platforms.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i< stage.Platforms.Count; i++)
        {
            spawnPosY -= LevelDistance;
            GameObject platform = Instantiate(helicPlatformPrefab, transform);
            platform.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedPlatforms.Add(platform);

            //Creating the Gaps
            int partsToDisable = 12 - stage.Platforms[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while(disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = platform.transform.GetChild(Random.Range(0, platform.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            //Creating the Death Parts
            List<GameObject> leftParts = new List<GameObject>();

            foreach(Transform t in platform.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }
            //Creating the Death Parts
            List<GameObject> deathParts = new List<GameObject>();

            while(deathParts.Count < stage.Platforms[i].deathPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }

        }
    }
}
