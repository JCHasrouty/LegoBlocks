using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // reference to the game object you want to generate
    public GameObject legoPlateUnitPref;

    //public GameObject legoBlockUniyPref;

    float x, z;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate a plate of legos
        for (int i = 0; i < 25; i++)
        {
            for(int j = 0; j < 25; j++)
            {
                // multiply by size of legos to make up for displacement so they hug each other
                x = i * 0.08f;
                z = j * 0.08f;
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
        
    }
}
