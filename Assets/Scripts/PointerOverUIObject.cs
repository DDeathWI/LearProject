using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverUIObject : MonoBehaviour {

    /// <summary>
    /// Check UI 
    /// </summary>
    public static bool IsPointerOverUIObject
    {
        get
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
