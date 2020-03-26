using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class resetPath2D : MonoBehaviour
{
    playerControlLerpSmoothAnim playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CharacterController))//dont think i need this anymore
        {
            if (other.CompareTag("Player"))
            {
                playerScript = other.gameObject.GetComponent<playerControlLerpSmoothAnim>();
                if (playerScript.in2D)
                {
                    playerScript.in2D = false;
                    playerScript.path = null;
                }
            }
        }
    }
}
