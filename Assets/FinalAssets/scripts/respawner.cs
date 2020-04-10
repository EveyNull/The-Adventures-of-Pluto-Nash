/**
 * Created by Jacob Woodman 10/04/2020
 * Designed to respawn power ups some time after collection
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawner : MonoBehaviour
{
    public GameObject item;
    public float respawnTime = 13;
    private bool collected = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("enemy")) && !collected)
        {
            Debug.Log("calling respawn");
            StartCoroutine(countdown());
        }
    }

    private IEnumerator countdown()
    {
        collected = true;
        yield return new WaitForSeconds(respawnTime);
        spawn();
    }

    void spawn()
    {
        GameObject spawnedItem = Instantiate(item, transform.position, transform.rotation);
        spawnedItem.transform.SetParent(transform);
        collected = false;
    }
}
