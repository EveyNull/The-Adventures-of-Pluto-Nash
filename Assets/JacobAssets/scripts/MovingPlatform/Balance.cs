using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    private GameObject player;
    public bool FakeBalance = true;
    public float playerForce = 5;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.SetParent(this.transform);
            Debug.Log("parented");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.SetParent(null);
            player = null;
            Debug.Log("unparented");
        }
    }
    
    private void Update()
    {
        if (player == null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime*2);
            return;
        }
        Vector3 pos = player.transform.localPosition;
        Quaternion quatChange = Quaternion.identity;
        
        if(!FakeBalance)
        quatChange *= Quaternion.Euler(pos.z *playerForce , 0, pos.x *-playerForce);
        else
        {
            if (pos.x > 0)
            {
                quatChange *= Quaternion.Euler(0, 0, -10);
            }
            else if (pos.x < 0)
            {
                quatChange *= Quaternion.Euler(0, 0, 10);
            }

            if (pos.z > 0)
            {
                quatChange *= Quaternion.Euler(10, 0, 0);
            }
            else if (pos.z < 0)
            {
                quatChange *= Quaternion.Euler(-10, 0, 0);
            }
        }


        transform.rotation = Quaternion.Slerp(transform.rotation, quatChange, Time.deltaTime);
    }
}
