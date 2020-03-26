using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotSpeed = 1f;
    [SerializeField] float moveSpeed = 1f;
    private Vector3 gravity;

    private void Update()
    {
        Turn();
        Move();
    }

    private void Turn()
    {
        Vector3 pog = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pog);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
    }

    private void Move()
    {
        transform.position += (transform.forward + gravity) * (Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("world"))
        {
            gravity = new Vector3(0, 1, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("world"))
        {
            gravity = new Vector3(0, -1, 0);
        }
    }
}
