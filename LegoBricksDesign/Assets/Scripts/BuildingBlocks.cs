using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlocks : MonoBehaviour
{
    int SelectedId = 0;

    private void OnEnable()
    {
        UIControl.OnBlockSelectedClicked += UIControl_OnBlockSelectedClicked;
        UIControl.OnColorSelectedClicked += UIControl_OnColorSelectedClicked;

    }
    private void UIControl_OnBlockSelectedCLicked1(int id)
    {
        throw new NotImplementedException();
    }
    private void UIControl_OnBlockSelectedClicked(int id)
    {
        Debug.Log($"You have selected Block Id; {id}");
        SelectedId = id;
    }
    private void UIControl_OnColorSelectedClicked(string colorHex)
    {
        ColorUtility.TryParseHtmlString($"#{colorHex}", out c);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Color c;
    // Update is called once per frame
    void Update()
    {


        // If mouse clicked spawn block
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            { 
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // 0.5f is the size difference of the cube so half of it won't be placed inside the floor
                cube.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
                if (hitInfo.transform.tag.Equals("Base"))
                {
                    cube.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
                }
                else
                {
                    if (hitInfo.normal == new Vector3(0, 1, 0))
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + 0.5f, hitInfo.transform.position.z);
                    else if (hitInfo.normal == new Vector3(1, 0, 0))
                        cube.transform.position = new Vector3(hitInfo.point.x + 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z);
                    else if (hitInfo.normal == new Vector3(0, 0, 1))
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + 0.5f);
                    else if (hitInfo.normal == new Vector3(0, -1, 0))
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - 0.5f, hitInfo.transform.position.z);
                    else if (hitInfo.normal == new Vector3(-1, 0, 0))
                        cube.transform.position = new Vector3(hitInfo.point.x - 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z);
                    else if (hitInfo.normal == new Vector3(0, 0, -1))
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - 0.5f);
                }
            }
        }
    }
}
