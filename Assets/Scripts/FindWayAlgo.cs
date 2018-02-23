﻿using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[Serializable]
public class FindWayAlgo
{
    private AttemptToMove attemptToMove;

    [SerializeField]
    public List<Way> wayList;

    private Vector2[] moves = new Vector2[] {

        new Vector2(0, 1),
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(1, 0)
    };


    public FindWayAlgo()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}

        attemptToMove = new AttemptToMove();

        wayList = new List<Way>();
    }

    public IEnumerator FindWay(Vector3 position, Vector3 destination)
    {

        //Add current position as Way
        wayList.Add(new Way(position));

        //Find Start Moves for startPosition
        for(int Fmove = 0; Fmove < moves.Length; Fmove++)
        {
            //CheckAttemptToMove
            if (attemptToMove.Result(position, moves[Fmove]))
            {
                //Add new Way to WayList
                wayList.Add(new Way(moves[Fmove], wayList[0]));
            }
        }
        
        //Remove StartPosition
        wayList.RemoveAt(0);

        // Search Way to Destination
        // Loop to end wayList 

        bool NoWay = true;

        for (int index = 0; index < wayList.Count; index++)
        {
            if (wayList[index].lastMove.x == 0)
            {
                moves = new Vector2[]{
                    new Vector2(1, 0),
                    new Vector2(-1, 0),
                    new Vector2(0, 1),
                    new Vector2(0, -1),
                };
            }
            else
            {
                moves = new Vector2[]{
                    new Vector2(0, -1),
                    new Vector2(0, 1),
                    new Vector2(-1, 0),
                    new Vector2(1, 0)
                };
            }

            // Loop Access moves
            for (int move = 0 ; move <moves.Length ; move++) 
            {
                //Check if Move return way Back
                if (wayList[index].lastMove == -moves[move])
                    continue;

                // Check if Can move to Vector2
                if (attemptToMove.Result(wayList[index].lastPosition, moves[move]))
                {
                    // Result PositionAfter Move
                    Vector2 vector2 = SumVector3Vector2(wayList[index].lastPosition, moves[move]);
                    Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0);

                    if (wayList.Exists(x => x.lastPosition == vector3))
                    {
                        continue;
                    }

                    if (wayList.Exists(x => x.points.Exists(y => y == vector2) && x.lenght <= wayList[index].lenght))
                    {
                        continue;
                    }

                    wayList.Add(new Way(moves[move], wayList[index]));
                }
                
            }

            

            if (wayList[index].lastPosition.x == destination.x && wayList[index].lastPosition.y == destination.y)
            {
                //wayList.RemoveAll(x => x.lastPosition != destination);

                CharacterControll.moves = wayList.Find(x => x.lastPosition == destination).points;

                CharacterControll.search = false;
                NoWay = false;

                break;
            }

            //wayList.RemoveAt(index);
            //index--;
           
        }


        if (NoWay)
        {
            Debug.Log("No Way. List: " + wayList.Count );

        }
        else {
            Debug.Log("List: " + wayList.Count );
        }

        Clear();
        CharacterControll.search = false;
        CharacterControll.s = true;

        yield return null;
    }

    public static Vector2 SumVector3Vector2(Vector3 vector3, Vector2 vector2) {

        return new Vector2(vector3.x + vector2.x, vector3.y + vector2.y);
    }

    public void Clear() {
        wayList.Clear();
    }
}