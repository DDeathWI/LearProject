using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.white;
    }


    private void OnMouseDown()
    {

        if (Input.GetMouseButton(0))
        {
#if UNITY_ANDROID
        if(Input.touchCount < 2)
            
#endif

            if (!EventSystem.current.IsPointerOverGameObject(0))
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

        //spriteRenderer.color = /*Random.ColorHSV();*/

        //spriteRenderer.color = Color.blue;


    }

    //private void OnMouseEnter()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        spriteRenderer.color = Random.ColorHSV();
    //    }
    //}
}
