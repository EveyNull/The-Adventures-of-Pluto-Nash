using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headCheck : MonoBehaviour
{
    private playerControlLerpSmoothAnim playerController;

    private void Start()
    {
        playerController = GetComponentInParent<playerControlLerpSmoothAnim>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("world"))
        {
            playerController.velocity.y = -5;
            playerController.anim.SetTrigger("endJump");
        }
    }
}
