using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private playerControlLerpSmoothAnim pc;

    private void Start()
    {
        pc = GetComponentInParent<playerControlLerpSmoothAnim>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        pc.groundStay(other);
    }

    //no longer colliding? no longer touching the ground
    private void OnTriggerExit(Collider other)
    {
        pc.groundExit(other);
    }
}
