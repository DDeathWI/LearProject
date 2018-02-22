using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class FindWayAlgo
{
    private AttemptToMove attemptToMove;

    [SerializeField]
    public List<Way> wayList;

    private Vector2[] moves = new Vector2[] {
        new Vector2( 0,1),
        new Vector2( 1,0),
        new Vector2(0, -1),
        new Vector2(-1,0)};

    public FindWayAlgo()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}

        attemptToMove = new AttemptToMove();

        wayList = new List<Way>();
    }

    public void FindWay(Vector3 position, Vector3 destination)
    {
        wayList.Clear();

        wayList.Add(new Way(position));

        for(int move = 0; move < moves.Length; move++)
        {
            if (attemptToMove.Result(position, moves[move]))
            {
                wayList.Add(new Way(moves[move], wayList[0]));
            }
        }

        wayList.RemoveAt(0);

        int listCount = wayList.Count;
        for (int index = 0; index < listCount; index++)
        {
            listCount = wayList.Count;
            if (listCount == 0)
                break;

            for (int move = 0; move < moves.Length; move++) {

                if (attemptToMove.Result(wayList[index].lastPosition, moves[move]))
                {
                    Vector2 vector2 = SumVector3Vector2(wayList[index].lastPosition, moves[move]);
                    Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0);

                    if (wayList.Exists(x => x.points.Exists(y => y == vector2) && x.lenght <= wayList[index].lenght))
                    {
                        continue;
                    }

                    if (wayList.Exists(x => x.lastPosition == vector3))
                    {
                        continue;
                    }

                    if (vector3 == wayList[index].lastPosition)
                    {
                        continue;
                    }

                    wayList.Add(new Way(moves[move], wayList[index]));
                }

                

                
            }
            if (wayList[index].lastPosition.x == destination.x && wayList[index].lastPosition.y == destination.y)
            {
                wayList.RemoveAll(x=>x.lastPosition != destination);

                CharacterControll.moves = wayList.Find(x => x.lastPosition == destination).points;

                CharacterControll.search = false;
                break;
            }

            wayList.RemoveAt(index);
            index--;

        }

        if (wayList.Count == 0)
        {
            CharacterControll.s = true;
            
            Debug.Log("No Way");
        }
        //waylist.clear();
        //charactercontroll.search = false;

    }

    public static Vector2 SumVector3Vector2(Vector3 vector3, Vector2 vector2) {

        return new Vector2(vector3.x + vector2.x, vector3.y + vector2.y);
    }

    public void Clear() {
        wayList.Clear();
    }
}
