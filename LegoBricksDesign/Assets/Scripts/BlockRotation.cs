using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    private float x_rot = 0;
    private float y_rot = 0;
    private float z_rot = 0;
    float speed;
    //Vector3 boundary_wall_1;
    //Vector3 boundary_wall_2;
    //Vector3 boundary_wall_3;
    //Vector3 boundary_wall_4;
    //Vector3 current_position;

    // Start is called before the first frame update
    void Start()
    {
        /*boundary_wall_1 = new Vector3((float)1.06, 0, 0);
        boundary_wall_2 = new Vector3((float)-1.06, 0, 0);
        boundary_wall_3 = new Vector3(0, 0, (float)1.06);
        boundary_wall_4 = new Vector3(0, 0, (float)-1.06);*/
    }

    // Update is called once per frame
    void Update()
    {
        //current_position = transform.position;
        var isMoving = CheckMovement();
        // check if key pressed and if block is not on the plate ye
        if (Input.GetKeyDown(KeyCode.R) && isMoving)
            transform.Rotate(x_rot, y_rot + 90, z_rot);

        //if(Input.GetKeyDown(KeyCode.W) && current_position.x < boundary_wall_1.x && current_position.x > boundary_wall_2.x && current_position.z < boundary_wall_3.z && current_position.z > boundary_wall_4.z)
        //{
        //    transform.Translate((float)0.1, 0, 0);
        //}
    }

    private bool CheckMovement()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude;
        // if object not moving return false, else return true
        if (speed < 0.5)
            return false;
        return true;
    }
}
