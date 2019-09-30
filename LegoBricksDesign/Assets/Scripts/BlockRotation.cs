using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    private float x_rot = 0;
    private float y_rot = 0;
    private float z_rot = 0;
    float speed;
    float blockMoveSpeed = 10;
    //private float horizontal_axis;
    //private float vertical_axis;
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
        //horizontal_axis = Input.GetAxis("Horizontal");
        //vertical_axis = Input.GetAxis("Vertical");
        //current_position = transform.position;
        var isMoving = CheckMovement();
        // check if key pressed and if block is not on the plate ye
        if (Input.GetKeyDown(KeyCode.R) && isMoving)
            transform.Rotate(x_rot, y_rot + 90, z_rot);


        // Block movement in Z axis
        if (Input.GetKeyDown(KeyCode.W) && isMoving)
            transform.Translate(Vector3.forward * blockMoveSpeed * Time.deltaTime);
        else if(Input.GetKeyDown(KeyCode.A) && isMoving)
            transform.Translate(Vector3.left * blockMoveSpeed * Time.deltaTime);
        else if (Input.GetKeyDown(KeyCode.S) && isMoving)
            transform.Translate(Vector3.back * blockMoveSpeed * Time.deltaTime);
        else if (Input.GetKeyDown(KeyCode.D) && isMoving)
            transform.Translate(Vector3.right * blockMoveSpeed * Time.deltaTime);
    }

    private bool CheckMovement()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude;
        // if object not moving return false, else return true
        if (speed < 0.5)
            return false;
        return true;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (Input.GetKeyDown(KeyCode.W) && CheckMovement())
        //    transform.Translate(Vector3.forward * -horizontal_axis);
        //else if (Input.GetKeyDown(KeyCode.A) && CheckMovement())
        //    transform.Translate(Vector3.left * -vertical_axis);
        //else if (Input.GetKeyDown(KeyCode.S) && CheckMovement())
        //    transform.Translate(Vector3.back * -horizontal_axis);
        //else if (Input.GetKeyDown(KeyCode.D) && CheckMovement())
        //    transform.Translate(Vector3.right * -vertical_axis);
    }
}
