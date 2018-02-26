using UnityEngine;

public class AttemptToMove 
{
    public bool Result(Vector3 position, Vector2 move) {

        RaycastHit2D hit = Physics2D.Raycast((new Vector2(position.x, position.y) + move / 1.9f), move, 0.5f);
        if (hit.collider != null)
        {
            return 8 != hit.collider.gameObject.layer && 9 != hit.collider.gameObject.layer;
        }
        return true;
    }


}
