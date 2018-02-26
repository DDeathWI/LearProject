using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGeneration : MonoBehaviour {


    [Header("Init Sprites Prefab")]
    [Space(5)]

    [SerializeField]
    private GameObject Ground;
    [SerializeField]
    private GameObject Water;
    [SerializeField]
    private GameObject Wall;



    [Space(5)]
    [Header("Set Character Prefab")]

    [SerializeField]
    private GameObject Character;

    [SerializeField]
    private int width = 8;
    [SerializeField]
    private int height = 8;

    [SerializeField]
    private int WaterChance = 10;

    private List<Vector3> MapList = new List<Vector3>();

    private Transform boardHolder;

    private Transform heroPosition;

    //
    public Text widthLabel;
    public Text heightLabel;
    public Text waterLabel;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Width") != 0)
        {
            width = PlayerPrefs.GetInt("Width");
            height = PlayerPrefs.GetInt("Height");
            WaterChance = PlayerPrefs.GetInt("Water");
        }
        DynamicGI.UpdateEnvironment();

        //Set Camera Position
        //SetCameraPosition();

        //Map Generation
        MapInit();

        //Set Hero
        SetHero();
    }

    void SetCameraPosition()
    {
        transform.position = new Vector3(width / 2, height / 2, -10);
    }

    void MapInit()
    {
        boardHolder = new GameObject("Board").transform;

        MapList.Clear();

        for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= height; j++)
            {
                

                GameObject toInstantiate = Ground;

                if (WaterChance > Random.Range(0, 100))
                {
                    toInstantiate = Water;
                }

                //Generate Walls
                if ( i== -1 || j == -1 || i == width|| j == height)
                {
                    toInstantiate = Wall;// Instantiate(Wall, new Vector3(i, j, 0), Quaternion.identity, WallsParent);
                }

                

                Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity, boardHolder);

            }
        }
    }
    
    void SetHero()
    {
        heroPosition = Instantiate(Character, new Vector3(0, 0, -0.5f), Quaternion.Euler(0,0,0)).transform;
        transform.position = heroPosition.position + new Vector3(-3.5f, -3.5f, -3.5f);

        transform.SetParent(heroPosition);

    }

    public void Reload() {

        //CharacterControll.SetDestination(heroPosition.position);
        //StopAllCoroutines();

        //CharacterControll cc = heroPosition.GetComponent<CharacterControll>();
        //cc.enabled = false;

        PlayerPrefs.SetInt("Width", width);
        PlayerPrefs.SetInt("Height", height);
        PlayerPrefs.SetInt("Water", WaterChance);

        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
    }

    Vector3 startPos;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        waterLabel.text = WaterChance.ToString();
        widthLabel.text = width.ToString();
        heightLabel.text = height.ToString();

        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(heroPosition.position, Vector3.back, (Input.mousePosition.x - startPos.x));
            startPos = Input.mousePosition;
        }

        if (Input.mouseScrollDelta.y + transform.position.z <= -0.5f && Input.mouseScrollDelta.y + transform.position.z >- 40)
        {
            transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y), heroPosition);
            transform.LookAt(heroPosition, Vector3.back);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }



    public void ChangeWidth(bool value)
    {
        width += value ? 1 : -1;
    }

    public void ChangeHeight(bool value)
    {
        height += value ? 1 : -1;
    }

    public void ChangeWater(bool value)
    {
        WaterChance += value ? 1 : -1;
    }
}
