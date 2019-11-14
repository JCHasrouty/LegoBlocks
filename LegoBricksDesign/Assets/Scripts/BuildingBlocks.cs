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
    Quaternion blockRotation;

    float x, z;
    float startX = -1f;
    float startZ = -1f;

    // x_offset used for double sized lego blocks
    float x_offset = 0.04f;
    float y_offset = 0.04f;

    int PlatformSize = 25;

    GameObject realTimeLego;
    GameObject legoBlockSpawn;
    RaycastHit realTimeHitInfo = new RaycastHit();

    public List<GameObject> legoUndo = new List<GameObject>();

    // layer mask to ignore
    //public LayerMask mask;

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

    //bool invertMask;
    //LayerMask newMask;

    // Update is called once per frame
    void Update()
    {
        ///// <summary>
        ///// Used to determine if we are over UI element or not.
        ///// </summary>
        ///// <returns></returns>
        ///

        if (IsPointerOverUIObject())
        {
            if (realTimeLego)
                Destroy(realTimeLego);

            return;
        }
        if(realTimeLego != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Rotating Lego");
                realTimeLego.transform.Rotate(Vector3.up, 90);
                blockRotation = realTimeLego.transform.rotation;
            }
        }

        //newMask = ~(invertMask ? ~mask.value : mask.value);

        bool realTimeHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out realTimeHitInfo);
        if (realTimeHit)
        {
            //if(!realTimeHitInfo.transform.name.Equals("TempCube"))
            //{
            if (realTimeLego == null)
            {
                realTimeLego = GameObject.Instantiate(legoBlockPrefabs[SelectedBlockID]);
                realTimeLego.name = "TempBlock";
                // Sets it to IgnoreRaycast layer
                //realTimeLego.layer = mask;
                realTimeLego.layer = 2;
                //realTimeLego.AddComponent<Rigidbody>().useGravity = false;
                realTimeLego.GetComponent<Rigidbody>().isKinematic = true;
                //realTimeLego.AddComponent<BlockCollision>();
                //realTimeLego.GetComponent<BoxCollider>().enabled = false;
                realTimeLego.GetComponent<Renderer>().material = transparentMaterial;
                realTimeLego.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
                realTimeLego.transform.position =
                    new Vector3(realTimeHitInfo.point.x,
                                realTimeHitInfo.point.y + y_offset,
                                realTimeHitInfo.point.z);

                realTimeLego.GetComponentInChildren<BlockRotation>().enabled = false;
            }
            else
            {
                // check to see if we 
                if (realTimeHitInfo.transform.tag.Equals("Base"))
                {
                    realTimeLego.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);

                    realTimeLego.transform.position = Vector3.Lerp(realTimeLego.transform.position,
                                                                    new Vector3(realTimeHitInfo.point.x,
                                                                    realTimeHitInfo.point.y + y_offset,
                                                                    realTimeHitInfo.point.z),
                                                                    Time.deltaTime * 10);
                    // Rotate works but lerp is still broken
                    //realTimeLego.transform.position = new Vector3(realTimeHitInfo.point.x,
                    //                                                realTimeHitInfo.point.y + (0.5f),
                    //                                                realTimeHitInfo.point.z);


                }
                else //if(realTimeHitInfo.transform.tag.Equals("MyCube"))
                {
                    realTimeLego.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.5f);
                    //if (realTimeHitInfo.normal == new Vector3(0, 0, 1))
                    //{
                    //    realTimeLego.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.transform.position.y, realTimeHitInfo.point.z + y_offset);
                    //}
                    //if (realTimeHitInfo.normal == new Vector3(1, 0, 0))
                    //{
                    //    realTimeLego.transform.position = new Vector3(realTimeHitInfo.point.x + y_offset, realTimeHitInfo.transform.position.y, realTimeHitInfo.transform.position.z);
                    //}
                    if (realTimeHitInfo.normal == new Vector3(0, 1, 0))
                    {
                        realTimeLego.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.point.y + y_offset, realTimeHitInfo.transform.position.z);
                    }
                    //if (realTimeHitInfo.normal == new Vector3(0, 0, -1))
                    //{
                    //    realTimeLego.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.transform.position.y, realTimeHitInfo.point.z - y_offset);
                    //}
                    //if (realTimeHitInfo.normal == new Vector3(-1, 0, 0))
                    //{
                    //    realTimeLego.transform.position = new Vector3(realTimeHitInfo.point.x - y_offset, realTimeHitInfo.transform.position.y, realTimeHitInfo.transform.position.z);
                    //}
                    //if (realTimeHitInfo.normal == new Vector3(0, -1, 0))
                    //{
                    //    realTimeLego.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.point.y - y_offset, realTimeHitInfo.transform.position.z);
                    //}
                }
            }
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


        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                legoBlockSpawn = GameObject.Instantiate(legoBlockPrefabs[SelectedBlockID]);
                legoUndo.Add(legoBlockSpawn);
                legoBlockSpawn.GetComponent<Rigidbody>().isKinematic = true;
                legoBlockSpawn.GetComponent<Renderer>().material.color = blockColor;
                legoBlockSpawn.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + y_offset, hitInfo.point.z);
                legoBlockSpawn.transform.rotation = blockRotation;
                if (hitInfo.transform.tag.Equals("Base"))
                {
                    if (legoBlockSpawn.GetComponent<BlockData>().Type == BlockType.doubleUnit)
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.transform.position.x + x_offset, hitInfo.point.y + 0.04f, hitInfo.transform.position.z);
                    }
                    else
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
                    }

                }
                else
                {
                    if (hitInfo.normal == new Vector3(0, 0, 1))
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + y_offset);
                    }
                    if (hitInfo.normal == new Vector3(1, 0, 0))
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.point.x + y_offset, hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 1, 0))
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 0, -1))
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - y_offset);
                    }
                    if (hitInfo.normal == new Vector3(-1, 0, 0))
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.point.x - y_offset, hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, -1, 0))
                    {
                        legoBlockSpawn.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - y_offset, hitInfo.transform.position.z);
                    }
                }
                //var block = GameObject.Instantiate(legoBlockPrefabs[SelectedBlockID]);
                //legoUndo.Add(block);
                //block.GetComponent<Rigidbody>().isKinematic = true;
                //block.GetComponent<Renderer>().material.color = blockColor;

                //block.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + y_offset, hitInfo.point.z);
                //block.transform.rotation = blockRotation;
                //if (hitInfo.transform.tag.Equals("Base"))
                //{
                //    if (block.GetComponent<BlockData>().Type == BlockType.doubleUnit)
                //    {
                //        block.transform.position = new Vector3(hitInfo.transform.position.x + x_offset, hitInfo.point.y + 0.04f, hitInfo.transform.position.z);
                //    }
                //    else
                //    {
                //        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
                //    }
                    
                //}
                //else
                //{
                //    if (hitInfo.normal == new Vector3(0, 0, 1))
                //    {
                //        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + y_offset);
                //    }
                //    if (hitInfo.normal == new Vector3(1, 0, 0))
                //    {
                //        block.transform.position = new Vector3(hitInfo.point.x + y_offset, hitInfo.transform.position.y, hitInfo.transform.position.z);
                //    }
                //    if (hitInfo.normal == new Vector3(0, 1, 0))
                //    {
                //        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + y_offset, hitInfo.transform.position.z);
                //    }
                //    if (hitInfo.normal == new Vector3(0, 0, -1))
                //    {
                //        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - y_offset);
                //    }
                //    if (hitInfo.normal == new Vector3(-1, 0, 0))
                //    {
                //        block.transform.position = new Vector3(hitInfo.point.x - y_offset, hitInfo.transform.position.y, hitInfo.transform.position.z);
                //    }
                //    if (hitInfo.normal == new Vector3(0, -1, 0))
                //    {
                //        block.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - y_offset, hitInfo.transform.position.z);
                //    }
                //}
            }
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            legoUndo.Remove(legoBlockSpawn);
            // works with or without gameObject
            Destroy(legoBlockSpawn.gameObject);
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //foreach (var result in results)
        //{
        //    Debug.Log(result.gameObject.name);
        //}
        return results.Count > 0;
    }
}

