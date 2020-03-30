using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_M : MonoBehaviour
{

    public Transform[] targets;
    public float Platform_Speed;
   GameObject player;
   GameObject platform;
    private int current;
    public bool P_Active;
    Vector3 PSCALE;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       platform = GameObject.FindGameObjectWithTag("Platform");

      

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (P_Active)
        {
            moving();
        }
    }

    void moving()
    {
       // Vector3 targer_dir = targets[current].position;
        if (transform.position != targets[current].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targets[current].position, Platform_Speed * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(pos);
           
           // Vector3.RotateTowards(transform.forward, targer_dir, 20000, 0.0f);
        }
        else
        {
            current = (current + 1) % targets.Length;
        }
        
    }

    private void OnTriggerEnter(Collider other)

    {
        if (other.gameObject.CompareTag("Player"))

        {
            // player.transform.parent = platform.gameObject.transform;
            //transform.parent = other.transform;
            // other.transform.parent = transform;
            other.transform.SetParent(transform);
          
            Debug.Log("childed");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("unpaired");
            //other.transform.parent = null;
            other.transform.SetParent(null);
        }
    }
}
