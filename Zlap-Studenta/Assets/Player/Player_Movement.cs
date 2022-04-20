using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private GameObject player = null;
    private Rigidbody player_object = null;
    private Vector3 moveDirection;
    private Vector3 mousePosition;
    private float player_x;
    private float player_z;
    [SerializeField] private float player_speed = 100;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        if (player_object == null)
        {
            player = GameObject.Find("Player");
            player_object = player.GetComponent<Rigidbody>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        player_x = player_object.transform.position.x;
        player_z = player_object.transform.position.z;

        Debug.Log("DIRECT X: " + moveDirection.x +" Z: " + moveDirection.z);
        Debug.Log("VELOCITY: " + player_object.velocity);

        if(Input.GetMouseButton(0))
        {
            PlayerInputs();
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
            player_object.velocity = new Vector3(0,0,0);
        }
    }

    void FixedUpdate()
    {
        Moving();
    }


    void PlayerInputs()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycasthit))
        {
            mousePosition = raycasthit.point;
        }
        moveDirection.x = ( mousePosition.x - player_x );
        moveDirection.z =  ( mousePosition.z - player_z );
        
        double length = Math.Sqrt( moveDirection.x * moveDirection.x  + moveDirection.z * moveDirection.z);
        moveDirection.x /= (float)length;
        moveDirection.z /= (float)length;
    }

    void Moving()
    {
        player_object.velocity = new Vector3(moveDirection.x * player_speed, 0, moveDirection.z * player_speed);
    }

}
