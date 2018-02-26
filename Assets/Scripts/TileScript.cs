using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    SpriteRenderer spriteRenderer;


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.white;
    }


   
        //spriteRenderer.color = /*Random.ColorHSV();*/

        //spriteRenderer.color = Color.blue;


    private void OnMouseUpAsButton()
    {
        Debug.Log("Here");


#if UNITY_ANDROID
        if (Input.touchCount < 2)

#endif
                if (!IsPointerOverUIObject())
                {
                    //do something
                    Debug.Log("Click");
                    CharacterControll.SetDestination(transform.position);
                }
                else
                {
                    Debug.Log("Block Click");
                    //do something else
                }

    }



    //private void OnMouseEnter()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        spriteRenderer.color = Random.ColorHSV();
    //    }
    //}
}
