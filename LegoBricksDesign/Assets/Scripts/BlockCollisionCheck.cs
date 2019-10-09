using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BlockCollisionCheck : MonoBehaviour
{
    public static bool allowMovement;

    // Start is called before the first frame update
    void Start()
    {
        allowMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        OnCollisionStay();
    }

    void OnCollisionStay()
    {
        allowMovement = false;
        EventManager.TriggerEvent("SpawnBlocks");
    }
}
