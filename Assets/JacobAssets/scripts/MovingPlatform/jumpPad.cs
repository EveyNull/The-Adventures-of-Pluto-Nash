using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{

    public float force = 36f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("jumpytime");
            other.GetComponent<playerControlLerpSmoothAnim>().velocity.y = force;
        }
    }
}
