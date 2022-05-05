using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirections { get; private set; }

    private void Start()
    {
        availableDirections = new List<Vector2>();
        Vector2 aaa;

        // We determine if the direction is available by box casting to see if
        // we hit a wall. The direction is added to list if available.

        CheckAvailableDirection(Vector2.up);

        aaa = new Vector2(1, Mathf.Sqrt(3));
        aaa.Normalize();
        CheckAvailableDirection(aaa);

        aaa = new Vector2(Mathf.Sqrt(3), 1);
        aaa.Normalize();
        CheckAvailableDirection(aaa);

        CheckAvailableDirection(Vector2.right);

        aaa = new Vector2(Mathf.Sqrt(3), -1);
        aaa.Normalize();
        CheckAvailableDirection(aaa);

        aaa = new Vector2(1, -Mathf.Sqrt(3));
        aaa.Normalize();
        CheckAvailableDirection(aaa);

        CheckAvailableDirection(Vector2.down);

        aaa = new Vector2(-1, -Mathf.Sqrt(3));
        aaa.Normalize();
        CheckAvailableDirection(aaa);
        
        aaa = new Vector2(-Mathf.Sqrt(3), -1);
        aaa.Normalize();
        CheckAvailableDirection(aaa);

        CheckAvailableDirection(Vector2.left);

        aaa = new Vector2(-Mathf.Sqrt(3), 1);
        aaa.Normalize();
        CheckAvailableDirection(aaa);

        aaa = new Vector2(-1, Mathf.Sqrt(3));
        aaa.Normalize();
        CheckAvailableDirection(aaa);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.125f, direction, 1f, obstacleLayer);
        

        // If no collider is hit then there is no obstacle in that direction
        if (hit.collider == null)
        {
            //Debug.DrawRay(transform.position, direction, Color.black, 3000f, false);
            availableDirections.Add(direction);
        }
    }

}
