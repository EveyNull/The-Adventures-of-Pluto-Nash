using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    public playerControlLerpSmoothAnim playerScript;
    bool blocked = false;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("world") && playerScript.in2D)
        {
            Debug.Log("Touch");
            playerScript.blocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("world") && playerScript.in2D)
        {
            Debug.Log("Touch");
            playerScript.blocked = false;
        }
    }
}
