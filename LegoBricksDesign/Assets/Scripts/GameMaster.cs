using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // reference to the plate 
    public GameObject legoPlateUnitPref;
    // reference to the various lego blocks
    public GameObject[] legoBlockUniyPref;
    // array to hold various material colors
    public Material[] blockMaterials;

    public float delta_time = 0.5f;
    float reset_time;

    float x, z;

    float startX = -1f;
    float startZ = -1f;

    static bool physicsOnOff;

    public int platform_size = 25;

    static bool platformPhysics;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate a plate of legos
        for (int i = 0; i < platform_size; i++)
        {
            for(int j = 0; j < platform_size; j++)
            {
                // multiply by size of legos to make up for displacement so they hug each other
                x = i * 0.08f + (startX + 0.04f);
                z = j * 0.08f + (startZ + 0.04f);
                // set location of plate legos and instantiate them
                var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        // dynamically create a game object at run time to be place at the center (0,0,0) by referencing the game object given to it
        //var gObj = GameObject.Instantiate(legoPlateUnitPref, new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        //#region Generate a block based on the reset time
        if(reset_time < Time.time)
        {
            // randomize and place game objects
            x = (Random.Range(0, platform_size) * 0.08f) + (startX + 0.04f);
            z = (Random.Range(0, platform_size) * 0.08f) + (startZ + 0.04f);
            var gObj = GameObject.Instantiate(legoBlockUniyPref[Random.Range(0, 2)], new Vector3(x, Random.Range(1, 3), z), Quaternion.identity);

            Material m = blockMaterials[Random.Range(0, 4)];
            foreach (var r in gObj.GetComponentsInChildren<Renderer>())
                r.material = m;

            //if (physicsOnOff)
                gObj.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ; ;

            // Destroy the objects spawning after a specific amount of time
            Destroy(gObj, delta_time + 15);
            reset_time = Time.time + delta_time;
        }
        //#endregion
    }
}

// If D is pressed turn iskinematics on to essentially break the platform.
// Kinematics on for the platform and the blocks
// They will collide and whatever happens happens then, you can toggle it on and off
