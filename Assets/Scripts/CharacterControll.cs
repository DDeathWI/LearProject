using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterControll : MonoBehaviour {

    public static Vector3 destination;

    public static List<Vector2> moves;

    [SerializeField]
    private AudioClip[] StepSound;

    private IEnumerator moveAction;

    private FindWayAlgo wayAlgo;

    private float timeCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        wayAlgo = new FindWayAlgo();

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;

        destination = transform.position;
    }

    private void Update()
    {
        if (AListener.instance.current == AListener.Action.DestinationSet)
        {
            timeCounter = Time.realtimeSinceStartup;
            moveAction = wayAlgo.FindWay(transform.position, destination);
            StartCoroutine(moveAction);

            AListener.instance.current = AListener.Action.SearchWay;
        }
        else if (wayAlgo.WayFind)
        {
            wayAlgo.WayFind = false;
            moveAction = LongMove();
            StartCoroutine(moveAction);

            AListener.instance.current = AListener.Action.InMove;
        }
        else if (wayAlgo.NoWay)
        {
            AListener.instance.current = AListener.Action.Idle;
        }
    }

    private IEnumerator LongMove() {
        Debug.Log("StartLongMove. Time: " + (Time.realtimeSinceStartup - timeCounter) + "Lenght "+ moves.Count);

        moveAction = LongMove();

        RaycastHit2D hit =
              Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
              new Vector2(transform.position.x, transform.position.y), 0.5f);

        hit.transform.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();

        for (int i = 0; i < moves.Count; i++)
        {
            audioSource.Stop();
            audioSource.clip = StepSound[0];
            audioSource.Play();

            yield return Move(new Vector3(moves[i].x, moves[i].y, transform.position.z));

            hit =
                Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), moves[i], 0.5f);

            hit.transform.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();

        }

        AListener.instance.current = AListener.Action.Idle;
    }

    private IEnumerator Move(Vector3 move)
    {
        for (float t = 0; t <= 1; t += 0.25f)
        {
            transform.position = Vector3.Lerp(transform.position, move, t);
            yield return new WaitForFixedUpdate();

        }
    }

    public static void SetDestination(Vector3 vector)
    {
        destination = vector;
    }

}
