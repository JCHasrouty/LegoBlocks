using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed;
    public float up_down_speed = 2;
    
    void Update()
    {
        RotateCamera();
        ZoomCamera();
        MoveCamera();
    }

    private void RotateCamera()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0, speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, (speed * Time.deltaTime) * -1, 0);
    }
    private void ZoomCamera()
    {
        float zoomChangeAmount = 40f;
        if (Input.mouseScrollDelta.y > 0)
            Camera.main.fieldOfView -= zoomChangeAmount * Time.deltaTime;
        if (Input.mouseScrollDelta.y < 0)
            Camera.main.fieldOfView += zoomChangeAmount * Time.deltaTime;
    }
    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            Camera.main.transform.position += Vector3.up * up_down_speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            Camera.main.transform.position += Vector3.down * up_down_speed * Time.deltaTime;
    }
}
