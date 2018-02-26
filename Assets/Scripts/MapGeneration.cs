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
    private int waterChance = 10;

    private List<Vector3> MapList = new List<Vector3>();

    private Transform boardHolder;

    private Transform heroPosition;

    [SerializeField]
    private Text widthLabel;
    [SerializeField]
    private Text heightLabel;
    [SerializeField]
    private Text waterLabel;


    private void Start()
    {
        AListener.instance.current = AListener.Action.Idle;


        //Get Map Si2e and waterChance
        if (PlayerPrefs.GetInt("Width") != 0)
        {
            width = PlayerPrefs.GetInt("Width");
            height = PlayerPrefs.GetInt("Height");
            waterChance = PlayerPrefs.GetInt("Water");
        }

        //Update Light after Reboot
        DynamicGI.UpdateEnvironment();

        //Map Generation
        MapInit();

        //Set Hero
        SetHero();
    }
    
    private void MapInit()
    {
        boardHolder = new GameObject("Board").transform;

        MapList.Clear();

        for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= height; j++)
            {
                // Default set Ground
                GameObject toInstantiate = Ground;

                // Look if water
                if (waterChance > Random.Range(0, 100))
                {
                    toInstantiate = Water;
                }

                // Generate Walls
                if ( i== -1 || j == -1 || i == width|| j == height)
                {
                    toInstantiate = Wall;
                }

                // Set Tile
                Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity, boardHolder);
            }
        }
    }
    
    void SetHero()
    {
        //Set Hero Position
        heroPosition = Instantiate(Character, new Vector3(0, 0, -0.5f), Quaternion.Euler(0,0,0)).transform;

        //SetCamera Position
        transform.position = heroPosition.position + new Vector3(-3.5f, -3.5f, -13.5f);

        //Set Camera parent hero
        transform.SetParent(heroPosition);
        transform.LookAt(heroPosition, Vector3.back);
    }

    /// <summary>
    /// Reload Scene 
    /// </summary>
    private void Reload() {
        
        //Set map si2e and waterChance
        PlayerPrefs.SetInt("Width", width);
        PlayerPrefs.SetInt("Height", height);
        PlayerPrefs.SetInt("Water", waterChance);

        
        //Load Scene
        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
    }


    // Mouse start Position
    private Vector3 startPos;
    private bool mouseClick;

    private void Update()
    {
        //Exit
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        // Label TextUI map si2e & waterChance
        waterLabel.text = waterChance.ToString();
        widthLabel.text = width.ToString();
        heightLabel.text = height.ToString();

        if (!PointerOverUIObject.IsPointerOverUIObject)
        {

            //Remember mousePosition
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                mouseClick = true;
            }

            if (Input.GetMouseButton(0) && mouseClick)
            {
                transform.RotateAround(heroPosition.position, Vector3.back, (Input.mousePosition.x - startPos.x));
                startPos = Input.mousePosition;
            }

            if (Input.mouseScrollDelta.y + transform.position.z <= -0.5f && Input.mouseScrollDelta.y + transform.position.z > -40)
            {
                transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y), heroPosition);
                transform.LookAt(heroPosition, Vector3.back);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseClick = false;
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
        waterChance += value ? 1 : -1;
    }


}
