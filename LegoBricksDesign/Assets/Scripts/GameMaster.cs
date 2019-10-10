using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // reference to the plate 
    public GameObject legoPlateUnitPref;
    // reference to the various lego blocks
    public GameObject[] legoBlockUnitPref;
    // array to hold various material colors
    public Material[] blockMaterials;

    public int[] RotationValues = {0, 90, 180, 270};

    public float DeltaTime = 3.5f;
    float ResetTime;

    float x, z;

    float startX = -1f;
    float startZ = -1f;

    static bool physicsOnOff;

    public int PlatformSize = 25;
    public int MaxHeight;

    public int PlatformSizeX;
    public int PlatformSizeY;

    static bool platformPhysics;
    string _blockSpeed = "";
    string _gameMode = "";

    //bool canSpawnBlock;

    // Start is called before the first frame update
    void Start()
    {
        //canSpawnBlock = true;
        // instantiate a plate of legos
        if (PlayerPrefs.HasKey("X") && PlayerPrefs.HasKey("Y"))
        {
            //PlatformSize = Convert.ToInt32(PlayerPrefs.GetString("X"));
            PlatformSizeX = Convert.ToInt32(PlayerPrefs.GetString("X"));
            PlatformSizeY = Convert.ToInt32(PlayerPrefs.GetString("Y"));

        }

        if (PlayerPrefs.HasKey("BlockSpeed"))
            _blockSpeed = PlayerPrefs.GetString("BlockSpeed");
        if (PlayerPrefs.HasKey("GameMode"))
            _gameMode = PlayerPrefs.GetString("GameMode");

        for (int i = 0; i < PlatformSizeX; i++)
        {
            for (int j = 0; j < PlatformSizeY; j++)
            {
                x = (i * 0.08f) + (startX + 0.04f);
                z = (j * 0.08f) + (startZ + 0.04f);
                // set location of plate legos and instantiate them
                var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(x, 0, z), Quaternion.identity);
                //gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                gObj.GetComponentInChildren<Rigidbody>().isKinematic = true;

                //Rigidbody gObj1 = gObj.GetComponent<Rigidbody>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {  
        if (ResetTime < Time.time)
        {
            /*
            // randomize and place game objects
            x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.04f);
            z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
            // Spawning game object from UnitPref array, choosing x,y,z coordinates and setting rotation
            // Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
            var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 6)], new Vector3(x, UnityEngine.Random.Range(4, 6), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));


            // This is a way to set the local scale for all of the objects instead of setting it manually
            //gObj.transform.localScale = gObj.transform.localScale * 10;


            //var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, max_height, z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
            // command not working
            gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            gObj.GetComponent<Rigidbody>().isKinematic = false;
            // Slow down the legos in order to allow user to move and rotate
            if (_blockSpeed == "slow")
                gObj.GetComponent<Rigidbody>().drag = 10;
            else if (_blockSpeed == "fast")
                gObj.GetComponent<Rigidbody>().drag = 5;

            var blockType = gObj.GetComponent<BlockData>().Type;
            Debug.Log(blockType);

            //if(BlockData.singleUnit)
            int orientation = RotationValues[UnityEngine.Random.Range(0, 3)];
            gObj.transform.rotation = Quaternion.AngleAxis(orientation, Vector3.up);

            switch (orientation)
            {
                case 0:
                    {
                        if (blockType == BlockType.singleUnit)
                        {
                            // Check Z coordinates
                            if (z >= 1.04)
                                z = z - 0.48f;
                            else if (z <= -1.44)
                                z = z + 0.48f;
                            // Check X coordinates
                            if (x >= 1.04)
                                x = x + 0.08f;
                            else if (x <= -1.04)
                                x = x + 0.08f;
                        }
                        else if(blockType == BlockType.doubleUnit)
                        {

                        }
                        break;
                    }
                case 90:
                    {
                        if (blockType == BlockType.singleUnit)
                        {
                            // Check Z coordinates
                            if (z >= 1.04)
                                z = z - 0.08f;
                            else if (z <= -1.04)
                                z = z + 0.08f;
                            // Check X coordinates
                            if (x >= 1.04)
                                x = x - 0.48f;
                            else if (x <= -1.44)
                                x = x + 0.08f;
                        }
                        else if (blockType == BlockType.doubleUnit)
                        {

                        }
                        break;
                    }
                case 180:
                    {
                        if (blockType == BlockType.singleUnit)
                        {
                            // Check Z coordinates
                            if (z >= 1.44)
                                z = z - 0.48f;
                            else if (z <= -1.04)
                                z = z + 0.48f;
                            // Check X coordinates
                            if (x >= 1.04)
                                x = x - 0.08f;
                            else if (x <= -1.04)
                                x = x + 0.08f;
                        }
                        else if (blockType == BlockType.doubleUnit)
                        {

                        }
                        break;
                    }
                case 270:
                    {
                        if (blockType == BlockType.singleUnit)
                        {
                            // Check Z coordinates
                            if (z >= 1.04)
                                z = z - 0.08f;
                            else if (z <= -1.04)
                                z = z + 0.08f;
                            // Check X coordinates
                            if (x >= 1.44)
                                x = x - 0.48f;
                            else if (x <= -1.04)
                                x = x + 0.48f;
                        }
                        else if (blockType == BlockType.doubleUnit)
                        {

                        }
                        break;
                    }
            }*/

            var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, legoBlockUnitPref.Length)]);
            gObj.name = string.Format("Block-{0}", ResetTime);
            if (_blockSpeed == "slow")
                gObj.GetComponent<Rigidbody>().drag = 10;
            else if (_blockSpeed == "fast")
                gObj.GetComponent<Rigidbody>().drag = 5;

            float size = gObj.GetComponent<BlockData>().Size;
            if (gObj.GetComponent<BlockData>() != null)
            {
                size = (gObj.GetComponent<BlockData>().Size * 0.08f);

                Debug.Log(size);
            }
            var blockType = gObj.GetComponent<BlockData>().Type;

            if(blockType == BlockType.doubleUnit)
            {
                x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.08f);
                z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
            }
            else
            {
                x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.04f);
                z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
            }
            
            int orientation = RotationValues[UnityEngine.Random.Range(0, 3)];
            

            gObj.transform.rotation = Quaternion.AngleAxis(orientation, Vector3.up);
            
            switch (orientation)
            {
                case 0:
                    {
                        if(blockType == BlockType.doubleUnit)
                        {
                            // Check Z offset and fix according to size
                            if (z + size - 0.08 > 1)
                                z = 1 - (z + size - 0.05f);
                            else if (z + size - 0.08 < -1)
                                z = -1 + (z + size - 0.05f);
                            // Check X offset and reset if out of bounds
                            if (x < -1)
                                x = -0.96f;
                            else if (x > 1)
                                x = 0.96f;
                        }
                        else
                        {
                            // Check Z offset and fix according to size
                            if (z + size - 0.04 > 1)
                                z = 1 - (z + size - 0.04f);
                            else if (z + size - 0.04 < -1)
                                z = -1 + (z + size - 0.04f);
                            // Check X offset and reset if out of bounds
                            if (x < -1)
                                x = -0.96f;
                            else if (x > 1)
                                x = 0.96f;
                        }
                        break;
                    }
                case 90:
                    {
                        if (blockType == BlockType.doubleUnit)
                        {
                            if (z > 1)
                                z = 0.96f;
                            else if (z < -1)
                                z = -0.96f;
                            if (x < -1)
                                x = -0.96f;
                            else if (x + size - 0.04 > 1)
                                x = 1 - (x + size - 0.08f);
                        }
                        else
                        {
                            if (z > 1)
                                z = 0.96f;
                            else if (z < -1)
                                z = -0.96f;
                            if (x < -1)
                                x = -0.96f;
                            else if (x + size - 0.04 > 1)
                                x = 1 - (x + size - 0.04f);
                        }
                        break;
                    }
                case 180:
                    {
                        if (blockType == BlockType.doubleUnit)
                        {
                            if (z > 1)
                                z = 0.96f;
                            else if (z < -1)
                                z = -1 + (z + size - 0.08f);
                            if (x > 1)
                                x = 0.96f;
                            else if (x < -1)
                                x = 0.96f;
                        }
                        else
                        {
                            if (z > 1)
                                z = 0.96f;
                            else if (z < -1)
                                z = -1 + (z + size - 0.04f);
                            if (x > 1)
                                x = 0.96f;
                            else if (x < -1)
                                x = 0.96f;
                        }
                            
                        break;
                    }
                case 270:
                    {
                        if (blockType == BlockType.doubleUnit)
                        {
                            if (z > 1)
                                z = 0.96f;
                            else if (z < -1)
                                z = -0.96f;
                            if (x > 1)
                                x = 0.96f;
                            else if (x < -1)
                                x = -1 + (x - size + 0.08f);
                        }
                        else
                        {
                            if (z > 1)
                                z = 0.96f;
                            else if (z < -1)
                                z = -0.96f;
                            if (x > 1)
                                x = 0.96f;
                            else if (x < -1)
                                x = -1 + (x - size + 0.04f);
                        }
                        break;
                    }
            }

            gObj.transform.position = new Vector3(x, UnityEngine.Random.Range(5, 8), z);
            gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            // Slow down the legos in order to allow user to move and 
            gObj.GetComponent<Rigidbody>().isKinematic = false;

            ////////////////////////////////////////////////
            // Randomize Materials on Objects
            ///////////////////////////////////////////////
            Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
            foreach (var r in gObj.GetComponentsInChildren<Renderer>())
                r.material = m;

            // Destroy the objects spawning after a specific amount of time
            Destroy(gObj, DeltaTime + 15);
            ResetTime = Time.time + DeltaTime;
        }

        QuitApplicationCheck();
    }

        public void QuitApplicationCheck()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q has been pressed quitting application.");
            Application.Quit();
        }
    }
    // Still working on implementing this
    private void BreakAwayGameMode()
    {

    }
}