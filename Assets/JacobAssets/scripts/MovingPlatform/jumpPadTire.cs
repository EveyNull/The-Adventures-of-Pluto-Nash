using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPadTire : MonoBehaviour
{

    public float force = 36f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tire"))
        {
            other.GetComponent<tireControl>().velocity.y = force;
        }
    }
}