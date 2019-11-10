using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingBlocks : MonoBehaviour
{
    // Material variables
    int SelectedColorIndex = 0;
    public Material transparentMaterial;
    public Material[] availableMaterials;
    //Material blockMaterial;
    // Set default color
    Color blockColor = Color.white;

    // Lego Block variables
    int SelectedBlockID = 0;
    public GameObject[] legoBlockPrefabs;
    public GameObject legoPlateUnitPref;

    float x, z;
    float startX = -1f;
    float startZ = -1f;

    float y_offset = 0.04f;

    int PlatformSize = 25;

    GameObject realTimeLego;
    RaycastHit realTimeHitInfo = new RaycastHit();

    private void OnEnable()
    {
        UIControl.OnBlockSelectedClicked += UIControl_OnBlockSelectedClicked;
        UIControl.OnColorSelectedClicked += UIControl_OnColorSelectedClicked;

    }
    private void OnDisable()
    {
        UIControl.OnBlockSelectedClicked -= UIControl_OnBlockSelectedClicked;
        UIControl.OnColorSelectedClicked -= UIControl_OnColorSelectedClicked;
    }
    private void UIControl_OnBlockSelectedClicked1(int id)
    {
        throw new NotImplementedException();
    }
    private void UIControl_OnBlockSelectedClicked(int id)
    {
        Debug.Log($"You have selected Block Id; {id}");
        SelectedBlockID = id;
    }
    private void UIControl_OnColorSelectedClicked(string colorHex)
    {
        Debug.Log($"You have selected color: {colorHex}");
        ColorUtility.TryParseHtmlString($"#{colorHex}", out blockColor);

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlatformSize; i++)
        {
            for (int j = 0; j < PlatformSize; j++)
            {
                x = (i * 0.08f) + (startX + 0.04f);
                z = (j * 0.08f) + (startZ + 0.04f);
                // set location of plate legos and instantiate them
                var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(x, 0, z), Quaternion.identity);
                //gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                gObj.GetComponentInChildren<Rigidbody>().isKinematic = true;
                gObj.tag = "Base";
            }
        }
        //blockMaterial = availableMaterials[SelectedBlockID];
    }

    // Update is called once per frame
    void Update()
    {

        //            //foreach (var rb in block.GetComponentsInChildren<Rigidbody>())
        //            //    rb.isKinematic = true;

        //            //foreach (var r in block.GetComponentsInChildren<Renderer>())
        //            //{
        //            //    // assign default white/transparent material

        //            //}
        //            //var block = GameObject.CreatePrimitive(PrimitiveType.block);
        //            // 0.5f is the size difference of the block so half of it won't be placed inside the floor
        //            //block.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.04f, hitInfo.point.z);
        //            //if (hitInfo.transform.tag.Equals("Base"))
        //            //{
        //            //    // get the transform of the object you are hitting not the point of the raycast. (transform.position.x)
        //            //    block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
        //            //}
        //            //else
        //            //{
        //            //    if (hitInfo.normal == new Vector3(0, 1, 0))
        //            //        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
        //            //    //else if (hitInfo.normal == new Vector3(1, 0, 0))
        //            //    //    block.transform.position = new Vector3(hitInfo.point.x + 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z);
        //            //    //else if (hitInfo.normal == new Vector3(0, 0, 1))
        //            //    //    block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + 0.5f);
        //            //    //else if (hitInfo.normal == new Vector3(0, -1, 0))
        //            //    //    block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - 0.5f, hitInfo.transform.position.z);
        //            //    //else if (hitInfo.normal == new Vector3(-1, 0, 0))
        //            //    //    block.transform.position = new Vector3(hitInfo.point.x - 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z);
        //            //    //else if (hitInfo.normal == new Vector3(0, 0, -1))
        //            //    //    block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - 0.5f);
        //            //}
        

        ///// <summary>
        ///// Used to determine if we are over UI element or not.
        ///// </summary>
        ///// <returns></returns>
        //private bool IsPointerOverUIObject()
        //{
        //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //    //foreach (var result in results)
        //    //{
        //    //    Debug.Log(result.gameObject.name);
        //    //}
        //    return results.Count > 0;
        //}
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                var block = GameObject.Instantiate(legoBlockPrefabs[SelectedBlockID]);
                block.GetComponent<Rigidbody>().isKinematic = true;
                block.GetComponent<Renderer>().material.color = blockColor;

                block.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + y_offset, hitInfo.point.z);
                if (hitInfo.transform.tag.Equals("Base"))
                {
                    block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
                }
                else
                {
                    if (hitInfo.normal == new Vector3(0, 0, 1))
                    {
                        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + y_offset);
                    }
                    if (hitInfo.normal == new Vector3(1, 0, 0))
                    {
                        block.transform.position = new Vector3(hitInfo.point.x + y_offset, hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 1, 0))
                    {
                        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 0, -1))
                    {
                        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - y_offset);
                    }
                    if (hitInfo.normal == new Vector3(-1, 0, 0))
                    {
                        block.transform.position = new Vector3(hitInfo.point.x - y_offset, hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, -1, 0))
                    {
                        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - y_offset, hitInfo.transform.position.z);
                    }
                }
            }
        }
    }
}

