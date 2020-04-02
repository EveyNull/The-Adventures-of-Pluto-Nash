using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {

            StartCoroutine(DestroyPlatform());
        }
    }

    IEnumerator DestroyPlatform()
    {
        yield return new  WaitForSeconds(1);
        
        Destroy(this.gameObject);
    }
}
