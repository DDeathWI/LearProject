using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Way
{
    /// <summary>
    /// Way length
    /// </summary>
    public int lenght;

    public Vector3 startPosition;

    public Vector3 lastPosition;

    public Vector2 lastMove;

    public List<Vector2> points;

    public Way(Vector3 position)
    {
        lenght = 0;

        startPosition = position;
        lastPosition = startPosition;

        lastMove = new Vector2(0, 0);

        points = new List<Vector2> {
            //position
        };
    }

    public Way(Vector2 move,  Way _way)
    {
        lenght = _way.lenght;
        startPosition = _way.startPosition;
        lastPosition = _way.lastPosition;

        lastMove = move;

        points = new List<Vector2>();
        for (int index = 0; index < _way.points.Count; index++)
        {
            points.Add(_way.points[index]);
        }


        lenght++;
        lastPosition = FindWayAlgo.SumVector3Vector2(lastPosition, move);

        points.Add(lastPosition);
    }

    public void AddMove(Vector2 move)
    {
        lenght++;

        lastPosition = FindWayAlgo.SumVector3Vector2(lastPosition, move);

        points.Add( lastPosition);
    }
    
    
}