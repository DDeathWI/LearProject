using UnityEngine;

public class AListener : MonoBehaviour {

    public static AListener instance;

    public enum Action
    {
        Idle,
        DestinationSet,
        SearchWay,
        WayFind,
        InMove
    };

    public Action current;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            current = Action.Idle;
        }

    }

}
