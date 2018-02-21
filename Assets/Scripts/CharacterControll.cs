using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterControll : MonoBehaviour {

    private IEnumerator moveAction;

    private static Vector3 destination;

    Vector2[] moves = new Vector2[] {
        new Vector2(-1,0),
        new Vector2( 1,0),
        new Vector2(0, 1),
        new Vector2(0,-1)};

    public List<Way> waysList;

    IEnumerator WaitingAction(Vector2 move)
    {
        if (CheckMoveAttempt(move))
        {
            Vector3 destination = transform.position + new Vector3(move.x, move.y, 0);
            transform.position = destination;
        }
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(moveAction);
        moveAction = null;
    }

    private void Awake()
    {
        destination = transform.position;

        waysList = new List<Way>(); 
    }

    private void Update()
    {
        if (moveAction != null)
            return;

        if (destination != transform.position)
        {
            FindWay();    
        }

        if (Input.GetKey(KeyCode.A)) {
            //Try go left
            moveAction = WaitingAction(new Vector2(-1,0));
            StartCoroutine(moveAction);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Try go right
            moveAction = WaitingAction(new Vector2(1, 0));
            StartCoroutine(moveAction);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //Try go up
            moveAction = WaitingAction(new Vector2(0, 1));
            StartCoroutine(moveAction);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Try go down
            moveAction = WaitingAction(new Vector2(0, -1));
            StartCoroutine(moveAction);
        }
    }

    private bool CheckMoveAttempt(Vector2 move) {

        RaycastHit2D hit = Physics2D.Raycast((new Vector2(transform.position.x, transform.position.y) + move/1.9f), move, 0.5f);
        if (hit.collider != null)
        {
            return 8 != hit.collider.gameObject.layer && 9 != hit.collider.gameObject.layer;
        }
        return true;

    }

    private bool CheckMoveAttempt(Vector3 position, Vector2 move)
    {

        RaycastHit2D hit = Physics2D.Raycast((new Vector2(position.x, position.y) + move / 1.9f), move, 0.5f);
        if (hit.collider != null)
        {
            return 8 != hit.collider.gameObject.layer && 9 != hit.collider.gameObject.layer;
        }
        return true;

    }

    public static void SetDestination(Vector3 vector)
    {
        destination = vector;
    }

    private void FindWay()
    {
        //if (waysList.Count == 0)
        //{
        //    for (int move = 0; move < moves.Length; move++)
        //    {
        //        if (CheckMoveAttempt(moves[move]))
        //        {
        //            Way _way = new Way(1, ) 
        //        }
        //    }
        //}

        for (int move = 0; move < moves.Length; move++)
        {
            foreach (Way _way in waysList)
            {   
                if (CheckMoveAttempt(_way.points[_way.points.Count-1], moves[move]))
                {
                    Debug.Log("!");
                }
            }
        }
    }

    [Serializable]
    public struct Way {
        public int length;
        public List<Vector2> points;

        public Way(int _length, Vector3 position) {
            length = _length;
            points = new List<Vector2>();
            points.Add(position);
        }
    }
}
