using UnityEngine;

public class TileScript : MonoBehaviour {
            
    private void OnMouseUpAsButton()
    {
#if UNITY_ANDROID
        if (Input.touchCount < 2)
#endif
            if (!PointerOverUIObject.IsPointerOverUIObject)
            {
                if(AListener.instance.current == AListener.Action.Idle)
                {
                    CharacterControll.SetDestination(transform.position);

                    AListener.instance.current = AListener.Action.DestinationSet;
                }
            }

    }

}
