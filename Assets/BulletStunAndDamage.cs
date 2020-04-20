using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStunAndDamage : MonoBehaviour
{
    int Damage = 1;
    public bool deathEnabled = true;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
        if (other.CompareTag("Player") || other.CompareTag("Blockade"))
        {
            Debug.Log("hit something tagged player");
            if (other.GetComponent<playerControlLerpSmoothAnim>())
            {
                other.GetComponent<playerControlLerpSmoothAnim>().Stun();
                other.GetComponent<Attacking>().TakeDamage(Damage);
            }

            if(deathEnabled) Destroy(transform.parent.gameObject);
        }
    }
}

