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
    public GameObject legoBlockReference;
    // array to hold various material colors
    public Material[] blockMaterials;

    public float[] RotationValues = {0, 90};

    public float delta_time = 3.5f;
    float reset_time;

    float x, z;

    float startX = -1f;
    float startZ = -1f;

    static bool physicsOnOff;

    public int platform_size = 25;

    public int plat_size_x;
    public int plat_size_y;

    static bool platformPhysics;
    string _blockSpeed = "";
    string _gameMode = "";

    bool canSpawnBlock;

    // Start is called before the first frame update
    void Start()
    {
        canSpawnBlock = true;
        // instantiate a plate of legos
        if (PlayerPrefs.HasKey("X") && PlayerPrefs.HasKey("Y"))
        {
            //platform_size = Convert.ToInt32(PlayerPrefs.GetString("X"));
            plat_size_x = Convert.ToInt32(PlayerPrefs.GetString("X"));
            plat_size_y = Convert.ToInt32(PlayerPrefs.GetString("Y"));

        }

        if (PlayerPrefs.HasKey("BlockSpeed"))
            _blockSpeed = PlayerPrefs.GetString("BlockSpeed");
        if (PlayerPrefs.HasKey("GameMode"))
            _gameMode = PlayerPrefs.GetString("GameMode");

        for (int i = 0; i < plat_size_x; i++)
        {
            for (int j = 0; j < plat_size_y; j++)
            {
                // multiply by size of legos to make up for displacement so they hug each other
                // Set to 8f - 4f if using 1 meter bricks
                // Create legos in maya using meters but set to .008 etc
                x = (i * 8f) + (startX + 4f);
                z = (j * 8f) + (startZ + 4f);
                // set location of plate legos and instantiate them
                var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(x, 0, z), Quaternion.identity);
                //gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                gObj.GetComponentInChildren<Rigidbody>().isKinematic = true;

                //Rigidbody gObj1 = gObj.GetComponent<Rigidbody>();
            }
        }
        // dynamically create a game object at run time to be place at the center (0,0,0) by referencing the game object given to it
        //var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(_gameMode == "uniform")
        {
            Debug.Log("Made it to uniform");
            StartCoroutine("UniformGameMode");
        }
        else if(_gameMode == "break-away")
        {
            Debug.Log("Made it to breakaway");
            StartCoroutine("BreakAwayGameMode");
        }*/
        //Debug.Log("Made it to none");
        //Generate a block based on the reset time
        //if(GameObject.FindObjectOfType<BlockRotation>().isCollided == true)
        if (reset_time < Time.time)
        {
            // randomize and place game objects
            // set the numbers to 0.1f
            x = (UnityEngine.Random.Range(0, platform_size) * 8f) + (startX + 4f);
            z = (UnityEngine.Random.Range(0, platform_size) * 8f) + (startZ + 4f);
            // Spawning game object from UnitPref array, choosing x,y,z coordinates and setting rotation
            // Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
            var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
            // command not working
            gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            // Slow down the legos in order to allow user to move and rotate
            if (_blockSpeed == "slow")
                gObj.GetComponent<Rigidbody>().drag = 10;
            else if (_blockSpeed == "fast")
                gObj.GetComponent<Rigidbody>().drag = 5;

            gObj.GetComponent<Rigidbody>().isKinematic = false;

            Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
            foreach (var r in gObj.GetComponentsInChildren<Renderer>())
                r.material = m;

            // THIS IS PHYSICS CONSTRAINT SO IF MAIN MENU IS CHECKED TO TRUE THEN SET THIS TO ON ELSE FALSE
            //if (physicsOnOff)
            //gObj.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            // Destroy the objects spawning after a specific amount of time
            Destroy(gObj, delta_time + 15);
            reset_time = Time.time + delta_time;
        }
        // Might need to put this in a different place
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
    // Functions to handle various game modes
    /*IEnumerator UniformGameMode()
    {
        var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
        //var gObj = GameObject.FindGameObjectsWithTag("Block"); 
        // wait until block reaches plate and is not allowed to move anymore
        canSpawnBlock = false;
        yield return new WaitUntil(() => gObj.GetComponent<BlockCollisionCheck>().allowMovement == true);
        canSpawnBlock = true;

        // If canSpawnBlock is true and allowMovement is false then spawn block
        if (canSpawnBlock == true && gObj.GetComponent<BlockCollisionCheck>().allowMovement == false)
        {
            x = (UnityEngine.Random.Range(0, platform_size) * 0.08f) + (startX + 0.04f);
            z = (UnityEngine.Random.Range(0, platform_size) * 0.08f) + (startZ + 0.04f);
            // Spawning game object from UnitPref array, choosing x,y,z coordinates and setting rotation
            // Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
            gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 1)], new Vector3(x, UnityEngine.Random.Range(5, 8), z), Quaternion.Euler(0.0f, RotationValues[UnityEngine.Random.Range(0, RotationValues.Length)], 0.0f));
            // command not working
            legoBlockReference.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            // Slow down the legos in order to allow user to move and rotate
            if (_blockSpeed == "slow")
                legoBlockReference.GetComponent<Rigidbody>().drag = 10;
            else if (_blockSpeed == "fast")
                legoBlockReference.GetComponent<Rigidbody>().drag = 5;

            legoBlockReference.GetComponent<Rigidbody>().isKinematic = false;

            Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
            foreach (var r in legoBlockReference.GetComponentsInChildren<Renderer>())
                r.material = m;
        }
        
        
    }
    private void BreakAwayGameMode()
    {

    }*/
}



// If D is pressed turn iskinematics on to essentially break the platform.
// Kinematics on for the platform and the blocks
// They will collide and whatever happens happens then, you can toggle it on and off

// Scale guide
//http://www.gamesmaderight.com/maya-to-unity-scale-guide/