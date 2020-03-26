using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireGrndCheck : MonoBehaviour
{
    private tireControl pc;

    private void Start()
    {
        pc = GetComponentInParent<tireControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        pc.GroundStay(other);
    }

    //no longer colliding? no longer touching the ground
    private void OnTriggerExit(Collider other)
    {
        pc.groundExit(other);
    }
}
