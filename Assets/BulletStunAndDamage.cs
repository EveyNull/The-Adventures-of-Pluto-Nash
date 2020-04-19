using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStunAndDamage : MonoBehaviour
{
    public bool deathEnabled = true;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit something tagged player");
            if (other.GetComponent<playerControlLerpSmoothAnim>())
            {
                other.GetComponent<playerControlLerpSmoothAnim>().Stun();
            }

            if(deathEnabled) Destroy(transform.parent.gameObject);
        }
    }
}

