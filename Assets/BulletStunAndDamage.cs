using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStunAndDamage : MonoBehaviour
{
    public bool deathEnabled = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerControlLerpSmoothAnim>().Stun();
            if(deathEnabled) Destroy(transform.parent.gameObject);
        }
    }
}

