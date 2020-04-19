using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)

    {
        if (other.gameObject.CompareTag("Player"))

        {

         
            other.transform.SetParent(transform);

            Debug.Log("childed");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            Debug.Log("unpaired");
            other.transform.SetParent(null);
        }
    }
}
