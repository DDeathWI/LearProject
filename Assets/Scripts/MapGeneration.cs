using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
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
        Transform heroPosition = Instantiate(Character, new Vector3(0, 0, -0.5f), Quaternion.Euler(0,0,0)).transform;

        //transform.parent = heroPosition;


    }

}
