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

    public float delta_time = 3.5f;
    float reset_time;

    float x, z;

    float startX = -1f;
    float startZ = -1f;

    static bool physicsOnOff;

    public int platform_size = 25;

    static bool platformPhysics;

    //Camera main_camera;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate a plate of legos
        if (PlayerPrefs.HasKey("X"))
        {
            platform_size = Convert.ToInt32(PlayerPrefs.GetString("X"));

        }

        for (int i = 0; i < platform_size; i++)
        {
            for (int j = 0; j < platform_size; j++)
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
        // dynamically create a game object at run time to be place at the center (0,0,0) by referencing the game object given to it
        //var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        //#region Generate a block based on the reset time
        if (reset_time < Time.time)
        {
            // randomize and place game objects
            x = (UnityEngine.Random.Range(0, platform_size) * 0.08f) + (startX + 0.04f);
            z = (UnityEngine.Random.Range(0, platform_size) * 0.08f) + (startZ + 0.04f);
            // Spawning game object from UniyPref array, choosing x,y,z coordinates and setting rotation
            // Edit Random.Range(50,1000) to alter Y coordinate to spawn higher
            var gObj = GameObject.Instantiate(legoBlockUnitPref[UnityEngine.Random.Range(0, 6)], new Vector3(x, UnityEngine.Random.Range(1, 3), z), Quaternion.identity);
            // command not working
            gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            gObj.GetComponent<Rigidbody>().isKinematic = false;

            Material m = blockMaterials[UnityEngine.Random.Range(0, 4)];
            foreach (var r in gObj.GetComponentsInChildren<Renderer>())
                r.material = m;

            //if (physicsOnOff)
            //gObj.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            // Destroy the objects spawning after a specific amount of time
            Destroy(gObj, delta_time + 15);
            reset_time = Time.time + delta_time;
        }
        //#endregion
        float x_rot = 0;
        float y_rot = 0;
        float z_rot = 0;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("This is executing now, Key press R");
            legoBlockUnitPref[0].transform.Rotate(x_rot, y_rot + 90, z_rot);
        }
        //RotateLego();
    }

    /*private void RotateLego()
    {
        float x_rot = 0;
        float y_rot = 0;
        float z_rot = 0;
        var gObj = GameObject.Find("_GameManager").name;
        if (Input.GetKeyDown(KeyCode.R))
            gObj.transform.Rotate(x_rot + 90, y_rot, z_rot);
        if (Input.GetKeyDown(KeyCode.T))
            gObj.transform.Rotate(x_rot, y_rot + 90, z_rot);
        if (Input.GetKeyDown(KeyCode.Y))
            gObj.transform.Rotate(x_rot, y_rot, z_rot + 90);
    }*/
}



// If D is pressed turn iskinematics on to essentially break the platform.
// Kinematics on for the platform and the blocks
// They will collide and whatever happens happens then, you can toggle it on and off

// Scale guide
//http://www.gamesmaderight.com/maya-to-unity-scale-guide/