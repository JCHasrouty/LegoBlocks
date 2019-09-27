using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    //public bool IsColliding;
    private void OnTriggerExit(Collider other)
    {
        //IsColliding = true;
        Destroy(other.gameObject);
    }
}
