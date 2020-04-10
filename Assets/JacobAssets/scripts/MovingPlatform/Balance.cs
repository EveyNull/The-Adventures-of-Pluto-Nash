using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    private GameObject player;
    public bool FakeBalance = true;
    public float playerForce = 5;
    private Quaternion originalRot;

    private void Start()
    {
        originalRot = transform.rotation;
        //not sure why this doesnt work, I hate quaternions so much
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.SetParent(null);
            player = null;
        }
    }

    private void Update()
    {
        if (player == null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRot, Time.deltaTime * 2);
            return;
        }

        Vector3 pos = player.transform.localPosition;
        Quaternion quatChange = Quaternion.identity;

        if (FakeBalance)
        {
            if (pos.x > 0)
            {
                quatChange *= Quaternion.Euler(0, originalRot.y, -10);
            }
            else if (pos.x < 0)
            {
                quatChange *= Quaternion.Euler(0, originalRot.y, 10);
            }

            if (pos.z > 0)
            {
                quatChange *= Quaternion.Euler(10, originalRot.y, 0);
            }
            else if (pos.z < 0)
            {
                quatChange *= Quaternion.Euler(-10, originalRot.y, 0);
            }
        }
        else
        {
            quatChange *= Quaternion.Euler(pos.z * playerForce, originalRot.y, pos.x * -playerForce);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, quatChange, Time.deltaTime);
    }
}
