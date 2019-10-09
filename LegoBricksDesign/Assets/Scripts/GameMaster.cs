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

    // new variables for spawn timer
    //public bool canSpawnBlock = true;
    //public float SpawnTime;
    //public float SpawnDelay;
    //private static bool isSpawnable;

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
                // multiply by size of legos to make up for displacement so they hug each other
                // Set to 8f - 4f if using 1 meter bricks
                // Create legos in maya using meters but set to .008 etc
                x = (i * 0.08f) + (startX + 0.04f);
                z = (j * 0.08f) + (startZ + 0.04f);
                // set location of plate legos and instantiate them
                var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(x, 0, z), Quaternion.identity);
                //gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                gObj.GetComponentInChildren<Rigidbody>().isKinematic = true;

                //Rigidbody gObj1 = gObj.GetComponent<Rigidbody>();
            }
        }

        //InvokeRepeating("SpawnBlocks", SpawnTime, SpawnDelay);
        // dynamically create a game object at run time to be place at the center (0,0,0) by referencing the game object given to it
        //var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("SpawnBlocks", SpawnTime, SpawnDelay);
        //    //if (_gameMode == "uniform")
        //    //{
        //    //    Debug.Log("Made it to uniform");
        //    //    StartCoroutine("UniformGameMode");
        //    //}
        //    //else if (_gameMode == "break-away")
        //    //{
        //    //    Debug.Log("Made it to breakaway");
        //    //    StartCoroutine("BreakAwayGameMode");
        //    //}
        //    //Debug.Log("Made it to none");
        //    //Generate a block based on the reset time
        //    //if(GameObject.FindObjectOfType<BlockRotation>().isCollided == true)
        //    //else
        //    //{
        //    //    Debug.Log("Made it into the else statement");

        /* My Code */
        //////////////////////////
        
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

            /* ------------------------------------------
               Professors New Code including block sizing
               ------------------------------------------ */
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
           
            x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.04f);
            z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
            int orientation = RotationValues[UnityEngine.Random.Range(0, 3)];
            var blockType = gObj.GetComponent<BlockData>().Type;

            gObj.transform.rotation = Quaternion.AngleAxis(orientation, Vector3.up);
            /*switch (orientation)
            {
                /*case 0:
                    {
                        //if (blockType == BlockType.singleUnit)
                        //{
                            // Check Z coordinates
                            /*if (z >= 1.04)
                                z = z - 0.48f;
                            else if (z <= -1.44)
                                z = z + 0.48f;
                            // Check X coordinates
                            if (x >= 1.04)
                                x = x + 0.08f;
                            else if (x <= -1.04)
                                x = x + 0.08f;
                            if (z <= -1)
                                z = -1 + ((size - 1 * 0.08f) + 0.04f);
                            else if (z >= 1)
                                z = 1 - ((size * 0.08f) + 0.04f);
                            if (x >= 1)
                                x = 0.96f;
                            else if (x <= -1)
                                x = -0.96f;

                        //}
                        //else if(blockType == BlockType.doubleUnit)
                        /*{
                            // Check Z coordinates
                            if (z >= 1.04)
                                z = z - 0.24f;
                            else if (z <= -1.2)
                                z = z + 0.24f;
                            // Check X coordinates
                            if (x >= 1.08)
                                x = x - 0.15f;
                            else if (x <= -1.08)
                                x = x + 0.15f;
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
                            // Check Z coordinates
                            if (z >= 1.08)
                                z = z - 0.15f;
                            else if (z <= -1.08)
                                z = z + 0.15f;
                            // Check X coordinates
                            if (x >= 1.04)
                                x = x - 0.24f;
                            else if (x <= -1.2)
                                x = x + 0.24f;
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
                            // Check Z coordinates
                            if (z >= 1.2)
                                z = z - 0.24f;
                            else if (z <= -1.04)
                                z = z + 0.24f;
                            // Check X coordinates
                            if (x >= 1.08)
                                x = x - 0.15f;
                            else if (x <= -1.08)
                                x = x + 0.15f;
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
                            // Check Z coordinates
                            if (z >= 1.08)
                                z = z - 0.15f;
                            else if (z <= -1.08)
                                z = z + 0.15f;
                            // Check X coordinates
                            if (x >= 1.2)
                                x = x - 0.24f;
                            else if (x <= -1.04)
                                x = x + 0.24f;
                        }
                        break;
                    }*/
                /*case 0:
                    {
                        if (z > 0.96)
                            z = (0.96f - ((size - 1) * 0.08f));
                        break;
                    }
                case 90:
                    {
                        if (x > 0.96)
                            x = (0.96f - ((size - 1) * 0.08f));
                        break;
                    }
                case 180:
                    {
                        if (z < -0.96)
                            z = (-0.96f + ((size - 1) * 0.08f));
                        break;
                    }
                case 270:
                    {
                        if (x < -0.96)
                            x = (-0.96f + ((size + 1) * 0.08f));
                        break;
                    }*/
            //}
            switch (orientation)
            {
                case 0:
                    {
                            // Check Z offset and fix according to size
                            if (z + size - 0.04 > 1)
                            {
                                z = 1 - (z + size - 0.04f);
                                if(blockType == BlockType.doubleUnit)
                                    x = x + 0.04f;
                            }    
                            else if (z + size - 0.04 < -1)
                            {
                                z = -1 + (z + size - 0.04f);
                                if (blockType == BlockType.doubleUnit)
                                {
                                    x = x + 0.04f;
                                }
                            }
                            // Check X offset and reset if out of bounds
                            if (x < -1)
                            {
                                x = -0.96f;
                                if (blockType == BlockType.doubleUnit)
                                {
                                    x = x + 0.04f;
                                }
                            }
                            else if (x > 1)
                            {
                                x = 0.96f;
                                if (blockType == BlockType.doubleUnit)
                                {
                                    x = x + 0.04f;
                                }
                            }
                        break;
                    }
                case 90:
                    {
                        if (z > 1)
                            z = 0.96f;
                        else if (z < -1)
                            z = -0.96f;
                        if (x < -1)
                        {
                            x = -0.96f;
                            if (blockType == BlockType.doubleUnit)
                            {
                                x = x + 0.04f;
                            }
                        }
                        else if (x + size - 0.04 > 1)
                        {
                            x = 1 - (x + size - 0.04f);
                            if (blockType == BlockType.doubleUnit)
                            {
                                x = x + 0.04f;
                            }
                        }
                        break;
                    }
                case 180:
                    {
                        if (z > 1)
                            z = 0.96f;
                        else if (z < -1)
                            z = -1 + (z + size - 0.04f);
                        if (x > 1)
                            x = 0.96f;
                        else if (x < -1)
                            x = 0.96f;
                        break;
                    }
                case 270:
                    {
                        if (z > 1)
                            z = 0.96f;
                        else if (z < -1)
                            z = -0.96f;
                        if (x > 1)
                            x = 0.96f;
                        else if(x < -1)
                            x = -1 + (x - size + 0.04f);
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

            // THIS IS PHYSICS CONSTRAINT SO IF MAIN MENU IS CHECKED TO TRUE THEN SET THIS TO ON ELSE FALSE
            //if (physicsOnOff)
            //gObj.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            // Destroy the objects spawning after a specific amount of time
            Destroy(gObj, DeltaTime + 15);
            ResetTime = Time.time + DeltaTime;
        }

        //if (ResetTime < Time.time)
        //{

        //    // randomize and place game objects
        //    // set the numbers to 0.1f
        //    x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.04f);
        //    z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
        //    // Spawning game object from UnitPref array, choosing x,y,z coordinates and setting rotation
        //    // Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
        //    var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
        //    // command not working
        //    gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        //    // Slow down the legos in order to allow user to move and rotate
        //    if (_blockSpeed == "slow")
        //        gObj.GetComponent<Rigidbody>().drag = 10;
        //    else if (_blockSpeed == "fast")
        //        gObj.GetComponent<Rigidbody>().drag = 5;

        //    gObj.GetComponent<Rigidbody>().isKinematic = false;

        //    Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
        //    foreach (var r in gObj.GetComponentsInChildren<Renderer>())
        //        r.material = m;

        //    // THIS IS PHYSICS CONSTRAINT SO IF MAIN MENU IS CHECKED TO TRUE THEN SET THIS TO ON ELSE FALSE
        //    //if (physicsOnOff)
        //    //gObj.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        //    // Destroy the objects spawning after a specific amount of time
        //    Destroy(gObj, DeltaTime + 15);
        //    ResetTime = Time.time + DeltaTime;
        //}
        // Might need to put this in a different place
        //QuitApplicationCheck();
    }

        public void QuitApplicationCheck()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q has been pressed quitting application.");
            Application.Quit();
        }
    }
    // Functions to handle various game modes
    //IEnumerator UniformGameMode()
    //{
    //    var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
    //    //var gObj = GameObject.FindGameObjectsWithTag("Block"); 
    //    // wait until block reaches plate and is not allowed to move anymore
    //    canSpawnBlock = false;
    //    yield return new WaitUntil(() => gObj.GetComponent<BlockCollisionCheck>().allowMovement == false);
    //    canSpawnBlock = true;
    //    SpawnBlocks();

    //    Destroy(gObj, DeltaTime + 15);
        //// If canSpawnBlock is true and allowMovement is false then spawn block
        //if (canSpawnBlock == true && gObj.GetComponent<BlockCollisionCheck>().allowMovement == false)
        //{
        //    x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.04f);
        //    z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
        //    // Spawning game object from UnitPref array, choosing x,y,z coordinates and setting rotation
        //    // Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
        //    gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
        //    // command not working
        //    legoBlockReference.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        //    // Slow down the legos in order to allow user to move and rotate
        //    if (_blockSpeed == "slow")
        //        legoBlockReference.GetComponent<Rigidbody>().drag = 10;
        //    else if (_blockSpeed == "fast")
        //        legoBlockReference.GetComponent<Rigidbody>().drag = 5;

        //    legoBlockReference.GetComponent<Rigidbody>().isKinematic = false;

        //    Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
        //    foreach (var r in legoBlockReference.GetComponentsInChildren<Renderer>())
        //        r.material = m;
        //}


    //}
    private void BreakAwayGameMode()
    {

    }

    //public void SpawnBlocks()
    //{
    //    isSpawnable = BlockCollisionCheck.allowMovement;
    //    //Debug.Log("Spawning Blocks");
    //    //// randomize and place game objects
    //    //// set the numbers to 0.1f
    //    //// check the 0.08 values and convert x,y to int32 and do platsize -1
    //    x = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startX + 0.04f);
    //    z = (UnityEngine.Random.Range(0, PlatformSize) * 0.08f) + (startZ + 0.04f);
    //    //// Spawning game object from UnitPref array, choosing x,y,z coordinates and setting rotation
    //    //// Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
    //    //var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
    //    //// command not working
    //    //gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    //    //// Slow down the legos in order to allow user to move and rotate
    //    //if (_blockSpeed == "slow")
    //    //    gObj.GetComponent<Rigidbody>().drag = 10;
    //    //else if (_blockSpeed == "fast")
    //    //    gObj.GetComponent<Rigidbody>().drag = 5;

    //    //gObj.GetComponent<Rigidbody>().isKinematic = false;

    //    Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));

    //    //Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
    //    //foreach (var r in gObj.GetComponentsInChildren<Renderer>())
    //    //    r.material = m;
    //    if (!isSpawnable)
    //        CancelInvoke("SpawnBlocks");
    //}
}



// If D is pressed turn iskinematics on to essentially break the platform.
// Kinematics on for the platform and the blocks
// They will collide and whatever happens happens then, you can toggle it on and off

// Scale guide
//http://www.gamesmaderight.com/maya-to-unity-scale-guide/
