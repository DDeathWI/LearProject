using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        CharacterControll.SetDestination(transform.position);

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
