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
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var isMoving = CheckMovement();
        // check if key pressed and if block is not on the plate yet
        if (Input.GetKeyDown(KeyCode.R) && isMoving)
            transform.Rotate(x_rot, y_rot + 90, z_rot);


        // Block movement in Z axis
        if (Input.GetKeyDown(KeyCode.W) && isMoving)
        {
            if (transform.eulerAngles.y == 0.0)
                transform.Translate(Vector3.forward * 0.08f);
            //transform.Translate(Vector3.forward * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 90.0)
                transform.Translate(Vector3.left * 0.08f);
            //transform.Translate(Vector3.back * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 180.0)
                transform.Translate(Vector3.back * 0.08f);
            else if (transform.eulerAngles.y == 270.0)
                transform.Translate(Vector3.right * 0.08f);
        }
        else if (Input.GetKeyDown(KeyCode.A) && isMoving)
        {
            if (transform.eulerAngles.y == 0.0)
                transform.Translate(Vector3.left * 0.08f);
            //transform.Translate(Vector3.forward * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 90.0)
                transform.Translate(Vector3.back * 0.08f);
            //transform.Translate(Vector3.back * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 180.0)
                transform.Translate(Vector3.right * 0.08f);
            else if (transform.eulerAngles.y == 270.0)
                transform.Translate(Vector3.forward * 0.08f);
            //transform.Translate(Vector3.left * blockMoveSpeed * Time.deltaTime);
        }    
        else if (Input.GetKeyDown(KeyCode.S) && isMoving)
        {
            if (transform.eulerAngles.y == 0.0)
                transform.Translate(Vector3.back * 0.08f);
            //transform.Translate(Vector3.forward * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 90.0)
                transform.Translate(Vector3.right * 0.08f);
            //transform.Translate(Vector3.back * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 180.0)
                transform.Translate(Vector3.forward * 0.08f);
            else if (transform.eulerAngles.y == 270.0)
                transform.Translate(Vector3.left * 0.08f);
            //transform.Translate(Vector3.left * blockMoveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.back * blockMoveSpeed * Time.deltaTime);
        }   
        else if (Input.GetKeyDown(KeyCode.D) && isMoving)
        {
            if (transform.eulerAngles.y == 0.0)
                transform.Translate(Vector3.right * 0.08f);
            //transform.Translate(Vector3.forward * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 90.0)
                transform.Translate(Vector3.forward * 0.08f);
            //transform.Translate(Vector3.back * blockMoveSpeed * Time.deltaTime);
            else if (transform.eulerAngles.y == 180.0)
                transform.Translate(Vector3.left * 0.08f);
            else if (transform.eulerAngles.y == 270.0)
                transform.Translate(Vector3.back * 0.08f);
            //transform.Translate(Vector3.right * blockMoveSpeed * Time.deltaTime);
        }
            

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
