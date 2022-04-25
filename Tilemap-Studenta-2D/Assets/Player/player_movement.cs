using System;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D player_object = null;
    private Vector3 moveDirection;
    private Vector3 mousePosition;
    private float player_x;
    private float player_y;
    [SerializeField] private float player_speed = 100;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        if (player_object == null)
        {
            player_object = GetComponent<Rigidbody2D>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        player_x = player_object.transform.position.x;
        player_y = player_object.transform.position.y;

        //Debug.Log("DIRECT X: " + moveDirection.x +" Z: " + moveDirection.z);
        //Debug.Log("VELOCITY: " + player_object.velocity);

        Debug.Log("DIRECT X: " + mousePosition.x + " Z: " + mousePosition.y);

        if (Input.GetMouseButton(0))
        {
            PlayerInputs();
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.y = 0;
            player_object.velocity = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        Moving();
    }


    void PlayerInputs()
    {

        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        

        moveDirection.x = (mousePosition.x - player_x);
        moveDirection.y = (mousePosition.y - player_y);

        double length = Math.Sqrt(moveDirection.x * moveDirection.x + moveDirection.y * moveDirection.y);
        moveDirection.x /= (float)length;
        moveDirection.y /= (float)length;



    }

    void Moving()
    {
        player_object.velocity = new Vector3(moveDirection.x * player_speed, moveDirection.y * player_speed, 0);
        mainCamera.transform.position = new Vector3(player_object.transform.position.x, player_object.transform.position.y, -10);
    }

}