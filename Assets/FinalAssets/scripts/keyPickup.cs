using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPickup : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
