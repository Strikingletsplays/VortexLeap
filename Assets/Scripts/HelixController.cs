using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    //For Helix Rotation
    private Vector2 _lastTapPos;
    private Vector3 _startRotation;
    private float delta;

    //Helix Platforms
    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helicPlatformPrefab;
    public List<GameObject> spawnedPlatforms = new List<GameObject>();
    
    //Creating Vertical Parts
    public List<GameObject> verticalParts = new List<GameObject>();

    //To load next stages
    public List<Stage> allStages = new List<Stage>();

    //Helix Distance
    private float helixDistance;

    //Ball Controller
    [SerializeField]
    private BallController _ballController;

    //Speed PowerUp
    [SerializeField]
    private GameObject _speedPowerUp;

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
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //current tap possition
            Vector2 curTapPos = Input.mousePosition;

            if (_lastTapPos == Vector2.zero)
            {
                _lastTapPos = curTapPos;
            }
            
            //how much you moved your finder tap?
            delta = _lastTapPos.x - curTapPos.x;
            _lastTapPos = curTapPos;

            //Rotate Helix (*0.35f to slow it down)
            transform.Rotate(Vector3.up * delta * 0.35f); // How fast will the cammera follow
        }
        if (Input.GetMouseButtonUp(0))
        {
            ResetLTP();
        }
    }
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        //Index of start of desabled parts
        int indexNum = Random.Range(0, 11);

        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber + " found in allStages List. Are all stages assined in the list?");
            return;
        }
        //SETTING COLORS FROM STAGE FILE
        Gamemanager.singleton.SetColors();

        //Reset helix rotation
        transform.localEulerAngles = _startRotation;

        //destroy old platforms if there exist
        foreach (GameObject go in spawnedPlatforms)
        {
            Destroy(go);
        }
        //destroy old Vertical Parts if there exist
        foreach (GameObject go in verticalParts)
        {
            Destroy(go);
        }
        //Recreate the platform & verticalPart list
        spawnedPlatforms = new List<GameObject>();
        verticalParts = new List<GameObject>();

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

            //Chek if not-bonus level
            if (!allStages[stageNumber].isBonus)
            {
                //Disabling parts in Platforms
                while (disabledParts.Count <= partsToDisable)
                {
                    int indexPart = Random.Range(0, platform.transform.childCount);
                    GameObject randomPart = platform.transform.GetChild(indexPart).gameObject;
                    if (!disabledParts.Contains(randomPart))
                    {
                        randomPart.SetActive(false);
                        disabledParts.Add(randomPart);

                        //if indexPart is not last, increment
                        if (indexPart != 11)
                        {
                            randomPart = platform.transform.GetChild(++indexPart).gameObject;
                            //if naibor not disabled
                            if (!disabledParts.Contains(randomPart))
                            {
                                //Disable part next to previous part
                                randomPart.SetActive(false);
                                disabledParts.Add(randomPart);
                            }
                            else
                            {
                                if (indexPart != 0)
                                {
                                    randomPart = platform.transform.GetChild(--indexPart).gameObject;
                                    if (!disabledParts.Contains(randomPart))
                                    {
                                        //Disable part before the previous part
                                        randomPart.SetActive(false);
                                        disabledParts.Add(randomPart);
                                    }
                                }   
                            }
                        }
                    }
                }
            }
            else //(BONUS LEVEL)
            {
                //indexNum number of part to start disabling
                int randomSeed = Random.Range(0, 3);
                //How big the gap will be (3 parts)
                for (int j = 0; j < 3; j++)
                {
                    GameObject partToDisable = platform.transform.GetChild(randomSeed).gameObject;
                    randomSeed++;
                    partToDisable.SetActive(false);
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

            while (deathParts.Count < stage.Platforms[i].deathPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!deathParts.Contains(randomPart))
                {
                    //Adding the Script "DeathPart" to the random part
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }

            //Spawn Vetrical Parts
            while (verticalParts.Count < stage.Platforms[i].verticalParts)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!verticalParts.Contains(randomPart))
                {
                    //Enable Vertical Part of platform
                    randomPart.transform.GetChild(0).gameObject.SetActive(true);
                    verticalParts.Add(randomPart);
                }
            }
        }
        //Call (SpawnBall)
        _ballController.SpawnBall();
    }
}
