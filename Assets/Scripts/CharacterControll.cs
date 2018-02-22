using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterControll : MonoBehaviour {

    private IEnumerator moveAction;

    private AttemptToMove attemptToMove;

    [SerializeField]
    private FindWayAlgo wayAlgo;

    public static Vector3 destination;

    public static bool s;

    public static List<Vector2> moves;

    public static bool search = false;

    IEnumerator LongMove() {
        Debug.Log("StartLongMove");

        moveAction = LongMove();

        for (int i = 0; i < moves.Count; i++)
        {
            //Debug.Log(transform.position);
            transform.position = new Vector3(moves[i].x, moves[i].y, transform.position.z);
            destination = transform.position;

            yield return new WaitForSeconds(0.15f);
        }

        wayAlgo.Clear();
        moves = null;
        moveAction = null;
        search = false;

    }

    IEnumerator WaitingAction(Vector2 move)
    {
        if (attemptToMove.Result(transform.position, move))
        {
            Vector3 _destination = transform.position + new Vector3(move.x, move.y, 0);
            transform.position = _destination;
            destination = _destination;
        }
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(moveAction);
        moveAction = null;
    }

    private void Awake()
    {
        destination = transform.position;

        wayAlgo = new FindWayAlgo();

        attemptToMove = new AttemptToMove();

        Camera.main.transform.position = transform.position + new Vector3(-3.5f, -3, -6);
        Camera.main.transform.parent = transform;
    }

    private void Update()
    {
        // In Move
        if (moveAction != null)
            return;

        if (moves != null)
        {
            StartCoroutine(LongMove());
        }

        if (s) {
            search = false;
            s = false;
            destination = transform.position;
        }

        //In Search Way
        if (search)
        {
            return;
        }

        Debug.Log("ready");

        //Destination Was Chanched
        if (destination != transform.position && !search)
        {
            wayAlgo.FindWay(transform.position, destination);
            search = true;
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
    
    public static void SetDestination(Vector3 vector)
    {
        destination = vector;
    }

    public static void SetSearch(bool value)
    {
        search = value;
    }
}
