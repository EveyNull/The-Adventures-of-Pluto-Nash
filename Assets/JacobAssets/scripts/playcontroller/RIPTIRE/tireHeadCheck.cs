using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireHeadCheck : MonoBehaviour
{
    private tireControl playerController;

    private void Start()
    {
        playerController = GetComponentInParent<tireControl>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("world"))
        {
            playerController.velocity.y = -5;
        }
    }
}