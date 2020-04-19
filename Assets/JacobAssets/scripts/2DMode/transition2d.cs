using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class transition2d : MonoBehaviour
{
    playerControlLerpSmoothAnim playerScript; 
    public PathCreator path;
    public float lastKnownPos = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
                playerScript = other.gameObject.GetComponent<playerControlLerpSmoothAnim>();
                if (!playerScript.in2D)
                {
                    playerScript.in2D = true;
                    playerScript.path = path;
                    playerScript.pathPosition = lastKnownPos;
                }/**
                else if (playerScript.in2D)
                {
                    playerScript.in2D = false;
                    playerScript.path = null;
                    lastKnownPos = playerScript.pathPosition;
                }*/
            }
    }
}
//known issue with creeping into transition box but not going to the other side
